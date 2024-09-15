import { CircularProgress } from "@mui/material";
import axios from "axios";
import  { useEffect, useState } from "react";
import { Outlet, useNavigate } from "react-router-dom";


function ProtectedRoute() {
    const [isloading, setLoading] = useState(false);
    const [currentUser, setCurrentUser] = useState(null)
    const navigate = useNavigate();

    const token = localStorage.getItem("token")

    if (!token) {
        navigate("/Login")
    }

    useEffect(() => {
        async function handleGetCurrentUser() {
            try {
                setLoading(true);
                const res = await axios({
                    method: "get",
                    url: `https://localhost:7007/api/Account/GetCurrentLoggedInUser?Token=${token}`,

                });

                if (res.status == 200) {
                    console.log(res)
                    setCurrentUser(res.data)
                    setLoading(false);
                }
            } catch (error: any) {
                console.log(error);
                setLoading(false);
            }
        }
        handleGetCurrentUser()

    }, [token])

    if (isloading) {
        return <CircularProgress color="info" />
    }

    if (currentUser == null && !isloading) {
        navigate("/Login")
    }

    if (currentUser) {
        //navigate("/")
        return <Outlet/>
    }
  
}

export default ProtectedRoute;