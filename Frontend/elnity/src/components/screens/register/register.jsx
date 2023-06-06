import { useEffect, useState } from "react";
import styles from "./Register.module.css"
import {useNavigate} from "react-router-dom"
import { ValidationService } from "../../services/ValidationService";
import axios from '../../api/Axios'

const API_URL = "/auth/register"

const Register = () => {
    const navigate = useNavigate();

    const [isLoading,setLoading] = useState(false);
    const [error,setError] = useState('')
    const [username,setUserName] = useState('')
    const [email,setEmail] = useState('')
    const [password,setPassword] = useState('')
    const [varifypassword,setVarifyPassword] = useState('')

    useEffect(() =>{
        setError("")
    },[username,email,password,varifypassword])

    const Register = async () =>{
        let validation_test = true 
        validation_test = ValidationService.validateEmail(email)
        if(!validation_test)
        {
            setError("Wrong email")
            return
        }
        validation_test = ValidationService.validatePassword(password)
        if(!validation_test)
        {
            setError("Password must be more 6 and less 20 symbols")
            return
        }
        if(password != varifypassword)
        {
            setError("Verify password wrong")
            return
        }
        setLoading(true)
        const response = await axios.post(API_URL,
            JSON.stringify({
                userName: username,
                password: password,
                email:email,
            }),
            {
                headers: {'Content-Type':'application/json'}
            }).catch((err) =>{
                setLoading(false)
                if(!err?.response){
                    setError("Server not responding")
                } else if (err.response?.status === 422) {
                    setError("Wrong data")
                } else if (err.response?.status === 500) {
                    setError("Server error")
                }
            })
            setLoading(false)
            if(response?.status === 200)
            {
                console.log(response?.data)
                navigate("/login");
            }
    }

    return (
        <div className={styles.background}>
            <div className={styles.page_content}>
                <div className={styles.register_window}>
                    <div className={styles.window_content}>
                        <h1 align="center" className={styles.registration_text}> Registration </h1>
                        <div className={styles["fieldset-container"]}>
                            <p className={styles.error}>{error}</p>
                            <label className={styles.label_input_full_name}>Full Name</label>
                            <div className={styles["input-area"]}>
                                <i className="fa fa-lg fa-user"></i>
                                <input type="text" name="user" id="user" placeholder="Enter full name" autoComplete="off" 
                                value={username} onChange={(e)=>setUserName(e.target.value)}/>
                            </div>

                            <label className={styles.label_input}>Email</label>
                            <div className={styles["input-area"]}>
                                <i className="fa fa-lg fa-envelope"></i>
                                <input type="text" name="email" id="email" placeholder="Enter email address" autoComplete="off" 
                                value={email} onChange={(e)=>setEmail(e.target.value)}/>
                            </div>

                            <label className={styles.label_input}>Password</label>
                            <div className={styles["input-area"]}>
                                <i className="fa fa-lg fa-lock"></i>
                                <input type="password" name="password" id="password" placeholder="Enter password" autoComplete="off" 
                                value={password} onChange={(e)=>setPassword(e.target.value)}/>
                            </div>

                            <label className={styles.label_input}>Verify password</label>
                            <div className={styles["input-area"]}>
                                <i className="fa fa-lg fa-lock"></i>
                                <input type="password" name="verify" id="verify" placeholder="Enter password" autoComplete="off" 
                                value={varifypassword} onChange={(e)=>setVarifyPassword(e.target.value)}/>
                            </div>
                        </div>

                        <div className={styles.buttons_area}>
                        {isLoading
                            ? <div className={styles.loader}></div> 
                            : <button className={styles.button_Register} type="button" onClick={Register}>
                                Register Now
                            </button>}
                            <button className={styles.button_Sign} type="button" onClick={()=> navigate("/login")}>
                                Sign In
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>)
}

export default Register