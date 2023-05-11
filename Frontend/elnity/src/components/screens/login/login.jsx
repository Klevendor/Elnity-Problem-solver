import styles from "./Login.module.css"
import {useNavigate} from "react-router-dom"


const Login = () => {
    const navigate = useNavigate();

    const handleRegisterClick = () => {
        navigate("/register");
    }


    return (<div className={styles.background}>
        <div className={styles.page_content}>
            <div className={styles.login_window}>
                <div className={styles.window_content}>
                    <h1 align="center" className={styles.registration_text}> Login </h1>
                    <div className={styles["fieldset-container"]}>
                        <label className={styles.label_input}>Email</label>
                        <div className={styles["input-area"]}>
                            <i className="fa fa-lg fa-envelope"></i>
                            <input type="text" name="email" id="email" placeholder="Enter your email" autoComplete="off" />
                        </div>

                        <label className={styles.label_input}>Password</label>
                        <div className={styles["input-area"]}>
                            <i className="fa fa-lg fa-lock"></i>
                            <input type="password" name="verify" id="verify" placeholder="Enter password" autoComplete="off" />
                        </div>
                    </div>

                    <div className={styles.buttons_area}>
                        <button className={styles.button_Login} type="button">
                            Login Now
                        </button>
                        <button className={styles.button_Sign} type="button" onClick={handleRegisterClick}>
                            Sign Up
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </div>)
}

export default Login