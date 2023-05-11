import styles from "./Register.module.css"
import {useNavigate} from "react-router-dom"

const Register = () => {
    const navigate = useNavigate();

    const handleLoginClick = () => {
        navigate("/login");
    }


    return (
        <div className={styles.background}>
            <div className={styles.page_content}>
                <div className={styles.register_window}>
                    <div className={styles.window_content}>
                        <h1 align="center" className={styles.registration_text}> Registration </h1>
                        <div className={styles["fieldset-container"]}>
                            <label className={styles.label_input}>Full Name</label>
                            <div className={styles["input-area"]}>
                                <i className="fa fa-lg fa-user"></i>
                                <input type="text" name="user" id="user" placeholder="Enter full name" autoComplete="off" />
                            </div>

                            <label className={styles.label_input}>Email</label>
                            <div className={styles["input-area"]}>
                                <i className="fa fa-lg fa-envelope"></i>
                                <input type="text" name="email" id="email" placeholder="Enter email address" autoComplete="off" />
                            </div>

                            <label className={styles.label_input}>Password</label>
                            <div className={styles["input-area"]}>
                                <i className="fa fa-lg fa-lock"></i>
                                <input type="password" name="password" id="password" placeholder="Enter password" autoComplete="off" />
                            </div>

                            <label className={styles.label_input}>Verify password</label>
                            <div className={styles["input-area"]}>
                                <i className="fa fa-lg fa-lock"></i>
                                <input type="password" name="verify" id="verify" placeholder="Enter password" autoComplete="off" />
                            </div>
                        </div>

                        <div className={styles.buttons_area}>
                            <button className={styles.button_Register} type="button">
                                Register Now
                            </button>
                            <button className={styles.button_Sign} type="button" onClick={handleLoginClick}>
                                Sign In
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>)
}

export default Register