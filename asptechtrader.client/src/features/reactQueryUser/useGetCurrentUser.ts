import { useQuery } from "@tanstack/react-query";
import axios from "axios";
import { useNavigate } from "react-router-dom";

// this is a custom-hook for fetching the symbols from db with reactQuery and axios
export const useGetCurrentUser = () => {
    const navigate = useNavigate();

    const token = localStorage.getItem("token")

    if (!token) {
        navigate("/Login")
    }

    const {
        data: currentUser,
        isLoading: isLoadingUser,
        error,
        isSuccess
    } = useQuery({
        queryKey: ["current-user"], // the queryKey is a unic key to identify the data in the cash
        queryFn: async () => await axios({
            method: "get",
            url: `https://localhost:7007/api/Account/GetCurrentLoggedInUser?Token=${token}`,
        }).then(res => res.data)
       
    });
    return { currentUser, isLoadingUser, error, isSuccess };
};
