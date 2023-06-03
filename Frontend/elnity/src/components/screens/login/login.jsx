import styles from "./Login.module.css"
import {useLocation, useNavigate} from "react-router-dom"
import axios from '../../api/Axios'
import { useEffect, useState } from "react";
import useAuth from "./../../hooks/useAuth"

const API_URL = "/auth/login";

const Login = () => {
    const navigate = useNavigate();

    const location = useLocation();
    const from = location.state?.from?.pathname || "/";

    const {setAuth} = useAuth();

    const [error,setError] = useState('');

    const [isLoading,setLoading] = useState(false);
    const [email,setEmail] = useState('');
    const [password,setPassword] = useState('');

    useEffect(() =>{
        setError("")
    },[email,password]);

    const Login = async () =>{
        if(email === "" || password === "")
        {
            setError("Fields can't be empty")
            return 
        }
        setLoading(true)
        const response = await axios.post(API_URL,
            JSON.stringify({
                email: email,
                password: password,
            }),
            {
                headers: {'Content-Type':'application/json'}
            }).catch((err) =>{
                setLoading(false)
                if(!err?.response){
                    setError("Server is not responding")
                } else if (err.response?.status === 422) {
                    setError("Wrong login or password")
                } else if (err.response?.status === 500) {
                    setError("Server error")
                }
            })
            setLoading(false)
            if(response?.status === 200)
            {
                setAuth({
                    email: response?.data?.email,
                    username: response?.data?.userName,
                    roles: response?.data?.userRoles,
                    accessToken: response?.data?.token
                })
                navigate("/home",{replace:true})
            }
    }


    return (<div className={styles.background}>
        <div className={styles.page_content}>
            <div className={styles.login_window}>
                <div className={styles.window_content}>
                    <h1 align="center" className={styles.registration_text}>Login</h1>
                    <div className={styles["fieldset-container"]}>
                        <p className={styles.error}>{error}</p>
                        <label className={styles.label_input_email}>Email</label>
                        <div className={styles["input-area"]}>
                            <i className="fa fa-lg fa-envelope"></i>
                            <input type="text" name="email" id="email" placeholder="Enter your email" autoComplete="off" 
                             value={email} onChange={(e)=>setEmail(e.target.value)}/>
                        </div>

                        <label className={styles.label_input_password}>Password</label>
                        <div className={styles["input-area"]}>
                            <i className="fa fa-lg fa-lock"></i>
                            <input type="password" name="verify" id="verify" placeholder="Enter password" autoComplete="off" 
                             value={password} onChange={(e)=>setPassword(e.target.value)}/>
                        </div>
                    </div>
                    <div className={styles.buttons_area}>
                    {isLoading
                    ? <div className={styles.loader}></div> 
                    :  <button className={styles.button_Login} type="button" onClick={Login}>
                            Login Now
                        </button>
                    }
                        <button className={styles.button_Sign} type="button" onClick={() => navigate("/register")}>
                            Sign Up
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </div>)
}

export default Login