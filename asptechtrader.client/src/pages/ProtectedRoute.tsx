/* eslint-disable @typescript-eslint/no-unused-vars */
import { CircularProgress } from "@mui/material";
import  { useEffect } from "react";
import { Outlet, useNavigate } from "react-router-dom";
import { useGetCurrentUser } from "../features/reactQueryUser/useGetCurrentUser";

// this is a tsx file with logic of Authenticating user when first entered the app
// its like a middelware
function ProtectedRoute() {
    const navigate = useNavigate();
    const { currentUser, isLoadingUser, isSuccess,error } = useGetCurrentUser()
    

    useEffect(() => {
        if (currentUser == null && !isLoadingUser) {
            navigate("/Login")
        }
    }, [currentUser, isLoadingUser, navigate])

    if (isLoadingUser) {
        return (
            <div className="bg-gray-600 h-[100vh] flex items-center justify-center">
                <CircularProgress color="info" size={50} />
            </div>
        )
    }

    if (currentUser) {
        //navigate("/")
        return <Outlet/>
    }
  
}

export default ProtectedRoute;