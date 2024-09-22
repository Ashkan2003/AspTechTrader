import { useMutation, useQueryClient } from "@tanstack/react-query";
import axios from "axios";
import toast from "react-hot-toast";

export const useSaleSymbol = () => {
    const queryClient = useQueryClient();

    const { mutate : saleSymbolMutation } = useMutation({
        mutationFn: (data: {
            symbolSalePrice: number,
            symbolSaleQuantity: number,
            userId: string,
            symbolId: string
        }) => {
            return axios({
                method: "put",
                url: "https://localhost:7007/api/UserSymbolProperty/SaleSymbol",
                headers: {
                    Authorization: `Bearer ${localStorage.getItem("token")}`
                },
                data: data
            });
        },
        onSuccess: () => {
            toast.success("سفارش فروش شما با موفقیت ثبت شد.");
            queryClient.invalidateQueries({ queryKey: ["current-user"] });
        },
        onError: (error) => {
            toast.error("در ثبت سفارش  فروش خطایی رخ داد.");
            console.log(error);
        },
    });
    return { saleSymbolMutation };
};
