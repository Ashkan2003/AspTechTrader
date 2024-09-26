import { useQuery } from "@tanstack/react-query";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import { UserType } from "../../types/types";

// this is a custom-hook for fetching the symbols from db with reactQuery and axios
export const useGetCurrentUser = () => {
    const navigate = useNavigate();

    const accesstToken = localStorage.getItem("accessToken")

    //if (!token) {
    //    //navigate("/Login")
    //}

    const {
        data: currentUser,
        isLoading: isLoadingUser,
        error,
        isSuccess
    } = useQuery < UserType>({
        queryKey: ["current-user"], // the queryKey is a unic key to identify the data in the cash
        queryFn: async () => await axios({
            method: "get",
            url: `https://localhost:7007/api/Account/GetCurrentLoggedInUser?Token=${accesstToken}`,
        }).then(res => res.data),
        enabled: () => {
            return accesstToken ? true : false
        },
        
    });
    return { currentUser, isLoadingUser, error, isSuccess };
};
