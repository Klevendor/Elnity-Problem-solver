import styles from "./Register.module.css"


const Register = () => {

    return (
        <div className={styles.page_content}>
            <div className={styles.register_window}>
                <div className={styles.window_content}>
                    <h1 align="center" className={styles.registration_text}> Registration </h1>
                    <div className={styles["fieldset-container"]}>
                        <label className={styles.label_input} for="user">Full Name</label>
                        <div className={styles["input-area"]}>
                            <i className="fa fa-lg fa-user"></i>
                            <input type="text" maxlength="255" name="user" id="user" placeholder="Enter full name" autocomplete="off"/>
                        </div>

                        <label className={styles.label_input} for="user">Email</label>
                        <div className={styles["input-area"]}>
                            <i class="fa fa-lg fa-envelope"></i>
                            <input type="text" maxlength="255" name="email" id="email" placeholder="Enter email address" autocomplete="off"/>
                        </div>

                        <label className={styles.label_input} for="user">Password</label>
                        <div className={styles["input-area"]}>
                            <i class="fa fa-lg fa-lock"></i>
                            <input type="text" maxlength="255" name="password" id="password" placeholder="Enter password" autocomplete="off"/>
                        </div>

                        <label className={styles.label_input} for="user">Verify password</label>
                        <div className={styles["input-area"]}>
                            <i class="fa fa-lg fa-lock"></i>
                            <input type="text" maxlength="255" name="verify" id="verify" placeholder="Enter password" autocomplete="off"/>
                        </div>
                    </div>

                    <div className={styles.buttons_area}>
                        <button className={styles.button_Register} type="button">
                            Register Now
                        </button>
                        <button className={styles.button_Sign} type="button">
                            Sign In
                        </button>
                    </div>
                </div>
            </div>
        </div>)
}

export default Register