import axios from "axios";
import { useNavigate } from "react-router-dom";

 const api = axios.create({
    baseURL: "https://localhost:7007/api"
 })

export const useAxciosResponseInterceptore=() => {
    const navigate = useNavigate();


    api.interceptors.response.use(response => response, async (error) => {
    const originalRequest = error.config;

    // If the error status is 401 and there is no originalRequest._retry flag,
    // it means the token has expired and we need to refresh it
    if (error.response.status === 401 && !originalRequest._retry) {
        originalRequest._retry = true;
        console.log("runnin..................")
        try {
            // get token and refreshToken
            const token = localStorage.getItem("accessToken")
            const refreshToken = localStorage.getItem("refreshToken")

            // make a request to server to get new accessToekn and refreshToken
            const res = await axios({
                method: "post",
                url: "https://localhost:7007/api/Account/GenerateNewToken",
                data: {
                    token: token,
                    refreshToken: refreshToken,
                },
            });
            localStorage.setItem("accessToken", res.data.accessToken)
            localStorage.setItem("accessTokenExpirationTime", res.data.accessTokenExpirationTime)
            localStorage.setItem("refreshToken", res.data.refreshToken)

            // Retry the original request with the new token
            originalRequest.headers.Authorization = `Bearer ${res.data.accessToken}`;
            return axios(originalRequest);
        } catch (error) {
            // Handle refresh token error or redirect to login
            console.log(error)
            navigate("/Login")

        }
    }
})
    return {api}
}

