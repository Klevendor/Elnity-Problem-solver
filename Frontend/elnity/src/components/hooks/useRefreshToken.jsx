import axios from "../api/Axios";
import useAuth from "./useAuth";

const API_URL = "/auth/refresh-token"

const useRefreshToken = () =>{
    const {setAuth} = useAuth();

    const refresh = async () =>{
        const response = await axios.get(API_URL);
        setAuth(prev => {
            return {...prev,email: response.data.email,roles: response.data.roles , accessToken: response.data.token}
        });
        return response.data.token;
    }

    return refresh; 
};

export default useRefreshToken;