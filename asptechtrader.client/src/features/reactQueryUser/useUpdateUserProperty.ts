import { useMutation, useQueryClient } from "@tanstack/react-query";
import axios from "axios";
import toast from "react-hot-toast";

export const useUpdateUserProperty = () => {
    const queryClient = useQueryClient();

    const { mutate: updateUserPropertyMutation } = useMutation({
        mutationFn: (data: {
            userId: string,
            userProperty: number
        }) => {
            return axios({
                method: "put",
                url: "https://localhost:7007/api/Users/updateUserProperty",
                
                data: data
            });
        },
        onSuccess: () => {
            toast.success("خرید نهایی باموفقیت انجام شد.");
            queryClient.invalidateQueries({ queryKey: ["current-user"] });
        },
        onError: (error) => {
            toast.error("در ثبت خرید نهایی خطایی رخ داد.");
            console.log(error);
        },
    });
    return { updateUserPropertyMutation };
};
