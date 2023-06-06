import { useNavigate } from "react-router-dom";
import styles from "./AppSearch.module.css"

const BASE_URL = "http://localhost:5223";

const AppSearch = ({appData}) =>{
    const navigate = useNavigate()


    return <div className={styles.app_with_label_container}>
    <div className={styles.app_container} onClick={()=>navigate(`preview/${appData.id}`)}>
       <img className={styles.img_app} src={BASE_URL+appData.imagePath} alt="" />
    </div>
    <label className={styles.app_title}>{appData.name}</label>
</div>
}

export default AppSearch;