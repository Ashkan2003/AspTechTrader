/* eslint-disable @typescript-eslint/no-explicit-any */
import CssBaseline from "@mui/material/CssBaseline";
import Paper from "@mui/material/Paper";
import SignLogo from "../ui/SignLogo";
import {
    Box,
    Button,
    Checkbox,
    CircularProgress,
    FormControlLabel,
    Grid,
    TextField,
    Typography,
} from "@mui/material";
import Copyright from "../ui/Copyright";
import { SubmitHandler, useForm } from "react-hook-form";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import { useState } from "react";
import toast from "react-hot-toast";
import { Link } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import axios from "axios";


interface FormData {
    email: string;
    password: string;
}

const schema = z.object({
    email: z.string().email({ message: "فرمت ایمیل باید درست باشد." }),
    password: z
        .string()
        .min(8, { message: "حداقل رمز عبور باید 8 کاراکتر باشد." }),
});

function Login() {
    const [loading, setLoading] = useState(false);
    const navigate = useNavigate();


    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm<FormData>({ resolver: zodResolver(schema) });


    const onSubmit: SubmitHandler<FormData> = async (data) => {
        try {
            setLoading(true);
            const res = await axios({
                method: "post",
                url: "https://localhost:7007/api/Account/Login",
                data: {
                    email: data.email,
                    password: data.password,
                },
            });

            if (res.status == 200) {
                navigate("/")
                setLoading(false);

                // store the newly generated jwt-token in localStorage
                localStorage.setItem("accessToken", res.data.accessToken)
                localStorage.setItem("accessTokenExpirationTime", res.data.accessTokenExpirationTime)
                // store the refreshToken
                localStorage.setItem("refreshToken", res.data.refreshToken)

                toast.success("ورود با موفقیت انجام شد.");
            }
        } catch (error: any) {
            toast.error("ورود ناموفق");
            console.log(error);
            setLoading(false);
        }
    };

  return (
      <Grid container component="main" sx={{ height: "100vh" }}>
          <CssBaseline />
          <Grid
              item
              xs={12}
              sm={8}
              md={5}
              xl={4}
              component={Paper}
              elevation={6}
              square
          >
              <Box
                  sx={{
                      my: 8,
                      mx: 7,
                      display: "flex",
                      flexDirection: "column",
                      // alignItems: "center",
                  }}
              >
                  <SignLogo />
                  <Box
                      component="form"
                      noValidate
                      onSubmit={handleSubmit(onSubmit)}
                      sx={{ mt: 1 }}
                  >
                      <TextField
                          color="info"
                          margin="normal"
                          fullWidth
                          id="email"
                          label="آدرس ایمیل"
                          autoComplete="email"
                          autoFocus
                          {...register("email")}
                      />
                      {errors.email?.message && (
                          <Typography color="red">{errors.email?.message}</Typography>
                      )}
                      <TextField
                          color="info"
                          margin="normal"
                          required
                          fullWidth
                          label="رمز عبور"
                          type="password"
                          id="password"
                          autoComplete="current-password"
                          {...register("password")}
                      />
                      {errors.password?.message && (
                          <Typography color="red">{errors.password?.message}</Typography>
                      )}
                      <FormControlLabel
                          control={<Checkbox value="remember" color="info" />}
                          label="مرا به خاطر بسپار"
                      />
                      <Button
                          type="submit"
                          color="info"
                          fullWidth
                          variant="contained"
                          disabled={loading}
                          sx={{ mt: 2, mb: 2 }}
                      >
                          {loading ? <CircularProgress size={25} color="info" /> : "ورود"}
                      </Button>

                      <Grid container>
                          <Grid item xs>
                              <Link to="#" className="text-blue-600">
                                  فراموشی کلمه عبور
                              </Link>
                          </Grid>
                          <Grid item>
                              <Link to="/Register" className="text-blue-600">
                                  ثبت نام
                              </Link>
                          </Grid>
                      </Grid>
                      <Copyright sx={{ mt: 5 }} />
                  </Box>
              </Box>
          </Grid>
          <Grid className="relative" item xs={false} sm={4} md={7} xl={8} sx={{}}>
              <img src="/p1.png"  alt="backgroundImg" />
          </Grid>
      </Grid>
     
  );
}

export default Login;
