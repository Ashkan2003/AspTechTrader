import Box from "@mui/material/Box";
import Container from "@mui/material/Container";
import CssBaseline from "@mui/material/CssBaseline";
import Copyright from "../ui/Copyright";
import SignLogo from "../ui/SignLogo";

import { zodResolver } from "@hookform/resolvers/zod";
import {
    Grid,
    TextField,
    FormControlLabel,
    Checkbox,
    Button,
    Typography,
    CircularProgress,
} from "@mui/material";
import axios from "axios";
import { useState } from "react";
import { SubmitHandler, useForm } from "react-hook-form";
import { z } from "zod";
import toast from "react-hot-toast";
import { Link, useNavigate } from "react-router-dom";

interface FormData {
    name: string;
    phoneNumber: number;
    email: string;
    password: string;
}

const schema = z.object({
    name: z.string().min(3, { message: "حداقل کاراکتر برای نام رعایت نشده." }),
    phoneNumber: z
        .string()
        .min(3, { message: "حداقل کاراکتر برای نام خانوادگی رعایت نشده" }),
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

        try {
            const res = await axios({
                method: "post",
                url: "https://localhost:7007/api/Account/Register",
                data: {
                    personName: data.name,
                    email: data.email,
                    password: data.password,
                    phoneNumber: data.phoneNumber,
                },
            });
            // if success return this
            if (res.status == 200) {
                // redirect the user to the dashboard
                navigate("/")

                // store the newly generated jwt-token in localStorage
                localStorage.setItem("token", res.data.token)
                // store the refreshToken
                localStorage.setItem("refreshToken", res.data.refreshToken)
                // store refreshTokenExpirationDateTime
                localStorage.setItem("refreshTokenExpirationDateTime", res.data.refreshTokenExpirationDateTime)

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
                              {...register("name")}
                          />
                          {errors.name?.message && (
                              <Typography color="red">{errors.name?.message}</Typography>
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
                          <Link to="/Login" className="text-blue-600">
                              حساب کاربری دارید؟ ورود
                          </Link>
                      </Grid>
                  </Grid>
              </Box>
          </Box>
          <Copyright sx={{ mt: 5 }} />
      </Container>
  );
}

export default Register;











//const [loading, setLoading] = useState(false);
//const navigate = useNavigate();

//// the resolver is a convectore thats convert the zod-schema to the useForm
//const {
//    register,
//    handleSubmit,
//    formState: { errors },
//} = useForm<FormData>({ resolver: zodResolver(schema) });

//// when the user click on the submit-btn then send these information to the db
//const onSubmit: SubmitHandler<FormData> = async (data) => {
//    setLoading(true);
//    console.log(data, 'the data')

//    try {
//        const res = await axios({
//            method: "post",
//            url: "https://localhost:7007/api/Account/Register",
//            data: {
//                personName: data.personName,
//                email: data.email,
//                password: data.password,
//                phoneNumber: data.phoneNumber,
//            },
//        });
//        // if success return this
//        if (res.status == 200) {
//            // redirect the user to the dashboard
//            navigate("/")
//            toast.success("ثبت نام با موفقیت انجام شد.");
//        }
//    } catch (error: any) {
//        console.log(error, "errors")
//        setLoading(false);
//        toast.error("ثبت نام ناموفق بود.");
//    }
//};