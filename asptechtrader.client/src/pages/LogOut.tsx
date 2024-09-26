import { Box, Button, Typography } from "@mui/material";
import { useNavigate } from "react-router-dom";

function LogOut() {
    const navigate=useNavigate()

    function handleLogOut() {
        console.log("logout...")
        localStorage.removeItem("accessToken")
        localStorage.removeItem("refreshToken")
        navigate("/Login")
    }

    return (
        <Box bgcolor="#2D3E4A" height="100vh" display="grid" justifyContent="center" alignItems="center" padding="170px">
          <Typography color="white" textAlign="center">از برنامه خارج میشوید؟</Typography>
          <Button
              type="submit"
                color="info"
                onClick={handleLogOut}
                variant="contained"
                sx={{  mb: 2, padding: 2, width:800 }}
          >
              LogOut
          </Button>
      </Box>
  );
}

export default LogOut;