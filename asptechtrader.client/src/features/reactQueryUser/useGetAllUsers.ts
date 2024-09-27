import { useQuery } from "@tanstack/react-query";
import axios from "axios";
import { UserType } from "../../types/types";

export const useGetAllUsers = () => {

    const {
        data: allUsers,
        isLoading: isLoadingAllUsers,
        error,
        isSuccess
    } = useQuery<UserType[]>({
        queryKey: ["all-users"], // the queryKey is a unic key to identify the data in the cash
        queryFn: async () => await axios({
            method: "get",
            url:"https://localhost:7007/api/Users/getAllUsers",
        }).then(res => res.data),

    });
    return { allUsers, isLoadingAllUsers, error, isSuccess };
};
