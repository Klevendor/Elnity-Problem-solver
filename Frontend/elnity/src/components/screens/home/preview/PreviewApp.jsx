import { useNavigate, useParams } from "react-router-dom";
import styles from "./PreviewApp.module.css"
import { useEffect, useState } from "react";
import { AppService } from "../../../services/AppService";
import useAxiosPrivate from "../../../hooks/useAxiosPrivate";
import useAuth from "../../../hooks/useAuth";
import useStore from "../../../hooks/useStore";

const BASE_URL = "http://localhost:5223";

const PreviewApp = () =>{
    const navigate = useNavigate()

    const {auth}  = useAuth()
    const axiosPrivate = useAxiosPrivate()
    const params = useParams()

    const [data,setData] = useState({})
    const {setAppHeader} = useStore()

    useEffect(()=>{
        setAppHeader("Preview")
        fetchData()
    },[])


    const fetchData = async () =>{
        const response = await AppService.getAppPreview(axiosPrivate,auth.email,params.id)
        console.log(response)
        setData(response)
    }

    const RegisterApp = async () =>{
        const response = await AppService.registerApp(axiosPrivate,auth.email,params.id)
        if(response)
            navigate("/home")
    }

    return <div className={styles.preview}>
        <div className={styles.preview_container}>
        <div className={styles.control_panel}>
                <i className={`fa-regular fa-circle-xmark ${styles.close}`} onClick={() => navigate("/home")}></i>
            </div>
            <div className={styles.image_container}>
                <img src={BASE_URL+data?.imagePath} alt="" className={styles.img_app}/>
            </div>
            <div className={styles.title_container}>
                <label className={styles.title_field}>Name:</label>
                <p className={styles.name}>{data?.name}</p>
            </div>
            <div className={styles.description_container}>
                <label className={styles.title_field}>Description:</label>
                <p className={styles.description}>{data?.description}</p>
            </div>
            <div className={styles.action_container}>
                {data?.inDevelop
                ? <div><p>App under development!</p></div>
                :  data?.currentUserAlredy
                    ? <button className={styles.join_button} onClick={()=>navigate("/home")}>Go home</button>
                    :<button className={styles.join_button} onClick={RegisterApp}>Get app</button>}
               
            </div>
        </div>
        </div>
}

export default PreviewApp;