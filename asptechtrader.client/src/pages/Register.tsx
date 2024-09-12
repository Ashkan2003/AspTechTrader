import { zodResolver } from "@hookform/resolvers/zod";
import {
    Box,
    Grid,
    TextField,
    FormControlLabel,
    Checkbox,
    Button,
    Typography,
    CircularProgress,
    Container,
    CssBaseline,
} from "@mui/material";
import axios from "axios";
import  { useState } from "react";
import {  SubmitHandler, useForm } from "react-hook-form";
import { z } from "zod";
import toast from "react-hot-toast";
import Copyright from "../ui/Copyright";
import SignLogo from "../ui/SignLogo";
import { Navigate } from "react-router-dom";
import { useNavigate } from "react-router-dom";


interface FormData {
    personName: string;
    phoneNumber: string;
    email: string;
    password: string;
}

const schema = z.object({
    perosnName: z.string().min(3, { message: "حداقل کاراکتر برای نام رعایت نشده." }),
    phoneNumber: z
        .string()
        .min(5, { message: "حداقل کاراکتر برای نام شماره تلفن رعایت نشده" }),
    email: z.string().email({ message: "فرمت ایمیل باید درست باشد." }),
    password: z
        .string()
        .min(8, { message: "حداقل رمز عبور باید 8 کاراکتر باشد." }),
});

function Register() {
    const [loading, setLoading] = useState(false);
    const navigate = useNavigate();

    // the resolver is a convectore thats convert the zod-schema to the useForm
    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm<FormData>({ resolver: zodResolver(schema) });

    // when the user click on the submit-btn then send these information to the db
    const onSubmit: SubmitHandler<FormData> = async (data) => {
        setLoading(true);
        console.log(data, 'the data')

        try {
            const res = await axios({
                method: "post",
                url: "https://localhost:7007/api/Account/Register",
                data: {
                    personName: data.personName,
                    email: data.email,
                    password: data.password,
                    phoneNumber: data.phoneNumber,
                },
            });
            // if success return this
            if (res.status == 200) {
                // redirect the user to the dashboard
                navigate("/")
                toast.success("ثبت نام با موفقیت انجام شد.");
            }
        } catch (error: any) {
            console.log(error, "errors")
            setLoading(false);
            toast.error("ثبت نام ناموفق بود.");
        }
    };

  return (
      <Container component="main" maxWidth="xs">
          <CssBaseline />
          <Box
              sx={{
                  marginTop: 8,
                  display: "flex",
                  flexDirection: "column",
                  alignItems: "center",
              }}
          >
              <SignLogo />
              <Box
                  component="form"
                  noValidate
                  onSubmit={handleSubmit(onSubmit)}
                  sx={{ mt: 3 }}
              >
                  <Grid container spacing={2}>
                      <Grid item xs={12} sm={6}>
                          <TextField
                              color="info"
                              autoComplete="given-name"
                              fullWidth
                              id="firstName"
                              label="نام"
                              autoFocus
                              {...register("personName")}
                          />
                          {errors.personName?.message && (
                              <Typography color="red">{errors.personName?.message}</Typography>
                          )}
                      </Grid>
                      <Grid item xs={12} sm={6}>
                          <TextField
                              color="info"
                              required
                              fullWidth
                              id="phoneNumber"
                              label="شماره تلفن"
                              autoComplete="phone-number"
                              {...register("phoneNumber")}
                          />
                          {errors.phoneNumber?.message && (
                              <Typography color="red">{errors.phoneNumber?.message}</Typography>
                          )}
                      </Grid>
                      <Grid item xs={12}>
                          <TextField
                              color="info"
                              required
                              fullWidth
                              id="email"
                              label="آدرس ایمیل"
                              autoComplete="email"
                              {...register("email")}
                          />
                          {errors.email?.message && (
                              <Typography color="red">{errors.email?.message}</Typography>
                          )}
                      </Grid>
                      <Grid item xs={12}>
                          <TextField
                              color="info"
                              required
                              fullWidth
                              label="رمز عبور"
                              type="password"
                              id="password"
                              autoComplete="new-password"
                              {...register("password")}
                          />
                          {errors.password?.message && (
                              <Typography color="red">{errors.password?.message}</Typography>
                          )}
                      </Grid>
                      <Grid item xs={12}>
                          <FormControlLabel
                              control={<Checkbox value="allowExtraEmails" color="info" />}
                              label="دریافت ایمیل از تک تریدر"
                          />
                      </Grid>
                  </Grid>
                  <Button
                      color="info"
                      type="submit"
                      fullWidth
                      variant="contained"
                      disabled={loading}
                      sx={{ mt: 3, mb: 2 }}
                  >
                      {loading ? <CircularProgress size={25} color="info" /> : "ثبت نام"}
                  </Button>
                  <Grid container justifyContent="flex-end">
                      <Grid item>
                          {/*<Navigate to="/login"/>*/}
                              حساب کاربری دارید؟ ورود
                      </Grid>
                  </Grid>
              </Box>
          </Box>
          <Copyright sx={{ mt: 5 }} />
      </Container>
  );
}

export default Register;