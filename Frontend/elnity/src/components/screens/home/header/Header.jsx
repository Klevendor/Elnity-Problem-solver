import styles from "./Header.module.css";
import useAuth from "../../../hooks/useAuth"
import useAxiosPrivate from "../../../hooks/useAxiosPrivate"
import { useNavigate } from "react-router-dom";


const Header = () => {
    const navigate = useNavigate()

    const {auth,setAuth} = useAuth()
    const axiosPrivate = useAxiosPrivate() 


    const Logout = async () =>{
        console.log("Вихід")
        var response = await axiosPrivate.get("/auth/logout").catch((err) => console.log(err))
        if(response.status===200)
            navigate("/login")
            setAuth()
    }


    return <div className={styles.header}>
        <div className={styles.menu_container}>
            menu
        </div>
        <div className={styles.logo_container}>
            <div className={styles.logo_container}>
                <img className={styles.logo_image} alt="" src="/logo.svg" />
                <div className={styles.logo}>
                    Elnity
                </div>
            </div>
        </div>
        <div className={styles.action_container}>
           <div className={styles.avatar_container}>
                <img className={styles.avatar} alt="" src="/default-avatar.png" />
           </div>
           <button className={styles.button_logout} onClick={Logout}>
                Logout
           </button>
        </div>
    </div>
}

export default Header;