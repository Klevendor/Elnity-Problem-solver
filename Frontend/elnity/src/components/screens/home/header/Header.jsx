import styles from "./Header.module.css";
import useAuth from "../../../hooks/useAuth"
import useAxiosPrivate from "../../../hooks/useAxiosPrivate"
import { useLocation, useNavigate } from "react-router-dom";
import { useEffect, useRef, useState } from "react";
import userStore from "./../../../hooks/useStore"

const Header = ({data}) => {
    const navigate = useNavigate()
    const location = useLocation()
    const ref = useRef()

    const [isMenuOpen,setIsMenuOpen] = useState(false)
    const {auth,setAuth} = useAuth()
    const axiosPrivate = useAxiosPrivate() 

    const [username,setUsername] = useState("")
    const [imagePath,setImagePath] = useState("/default-avatar.png")
    const {appHeader} = userStore()

    useEffect(()=>{
        setUsername(data?.username)
        setImagePath(data?.avatarPath)
    },[])

    const Logout = async () =>{
        console.log("Вихід")
        var response = await axiosPrivate.get("/auth/logout").catch((err) => console.log(err))
        if(response.status===200)
            navigate("/login")
            setAuth()
    }

    useEffect(() => {
        const checkIfClickedOutside = e => {
          if (isMenuOpen && ref.current && !ref.current.contains(e.target)) {
            setIsMenuOpen(false)
          }
        }
    
        document.addEventListener("mousedown", checkIfClickedOutside)
    
        return () => {
          document.removeEventListener("mousedown", checkIfClickedOutside)
        }
      }, [isMenuOpen])


    return <div className={styles.header}>
        <div className={styles.menu_container}>
            {appHeader}
        </div>
        <div className={styles.logo_container}>
            <div className={styles.logo_container}>
                <img className={styles.logo_image} alt="" src="/logo.svg" />
                <div className={styles.logo} onClick={()=>location.pathname != "/home"? navigate("/home") : console.log("alredy in /home")}>
                    Elnity
                </div>
            </div>
        </div>
        <div className={styles.action_container}>
           <div className={styles.avatar_container}>
                <img className={styles.avatar} alt="" src={imagePath} onClick={()=>setIsMenuOpen(!isMenuOpen)}/>
               
               <div ref={ref} className={`${styles.popup_menu} ${!isMenuOpen && styles.dialog_none}`}>
                    <div className={styles.user_info_container}>
                        <img className={styles.avatar_popup} alt="" src={imagePath} />
                        <p className={styles.username_label}>{username}</p>
                    </div>
                    <div className={styles.buttons_container}>
                        <img className={styles.profile} alt="" src="/image-9@2x.png"/>
                        <div className={styles.profile_text} onClick={()=>location.pathname != "/home/profile"? navigate("profile") : console.log("alredy in profile")}>Edit profile</div>
                    </div>
                </div>
                
           </div>
           <button className={styles.button_logout} onClick={Logout}>
                Logout
           </button>
        </div>
    </div>
}

export default Header;