import { Grid, Typography } from "@mui/material";

const SignLogo = () => {
  return (
    <Grid container alignItems="center" justifyContent="center">
      <Grid item>
        <img src="/Trade-brand.png" alt="brand" width="90" height="60" />
      </Grid>
      <Typography align="center" component="h1" fontWeight="bold" variant="h5">
        تک تریدر
      </Typography>
    </Grid>
  );
};

export default SignLogo;
