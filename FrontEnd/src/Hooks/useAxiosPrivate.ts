import { useEffect } from "react";
import {useAuth} from "../Context/useAuth.tsx";
import {axiosPrivate} from "../API/api.ts";

function useAxiosPrivate() {
    const { token, logout } = useAuth();

    useEffect(() => {
        const requestIntercept = axiosPrivate.interceptors.request.use(
            (config) => {
                if (!config.headers["Authorization"]) {
                    config.headers[
                        "Authorization"
                        ] = `Bearer ${token}`;
                }
                return config;
            },
            (error) => Promise.reject(error)
        );

        const responseIntercept = axiosPrivate.interceptors.response.use(
            (response) => response,
            async (error) => {
                const prevRequest = error?.config;
                if (error?.response?.status === 401 && !prevRequest?.sent) {
                    logout();
                    // prevRequest.sent = true;
                    // const newAccessToken = await refresh();
                    // prevRequest.headers[
                    //     "Authorization"
                    //     ] = `Bearer ${newAccessToken}`;
                    // return axiosPrivate(prevRequest);
                }
                return Promise.reject(error);
            }
        );

        return () => {
            axiosPrivate.interceptors.request.eject(requestIntercept);
            axiosPrivate.interceptors.response.eject(responseIntercept);
        };
    }, [token]);

    return axiosPrivate;
}

export default useAxiosPrivate;
