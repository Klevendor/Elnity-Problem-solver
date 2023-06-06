import { useLocation, useNavigate } from "react-router-dom";
import styles from "./SidebarApp.module.css"
import { useEffect } from "react";

const BASE_URL = "http://localhost:5223";

const SidebarApp = ({appData}) =>{
    const location = useLocation()
    const navigate = useNavigate()

    return <div className={styles.img_container} onClick={()=>location.pathname == `/home/app/${appData.id}`? console.log("alredy app") : navigate(`app/${appData.id}`)}>
       <img className={styles.img_app} src={BASE_URL+appData.imagePath} alt="" />
    </div>

}

export default SidebarApp;