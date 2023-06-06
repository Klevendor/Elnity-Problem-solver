import { useEffect, useRef, useState } from "react";
import styles from "./Apps.module.css";
import DialogSearch from "./DialogSearch"
import { AppService } from "../../../services/AppService";
import useAxiosPrivate from "../../../hooks/useAxiosPrivate";
import useAuth from "../../../hooks/useAuth";
import SidebarApp from "./SidebarApp";
import useStore from "../../../hooks/useStore";
import { Routes,Route } from "react-router-dom";
import NoteAggregator from "./functional/NoteAggregator";

const Apps = () => {
    const ref = useRef()
    const {auth} = useAuth()
    const axiosPrivate  = useAxiosPrivate()
    const [isLoading,setIsLoading]=useState(false)

    const [allApps,setAllApps] = useState()
    const [userApps,setUserApps] = useState()

    const {setAppHeader} = useStore()

    useEffect(()=>{
        setAppHeader("Home")
        fetchAllApps()
        fetchUserApps()
    },[])

    const fetchAllApps = async () =>{
        const response = await AppService.getApps(axiosPrivate)
        setAllApps(response)
    }

    const fetchUserApps = async () =>{
        const response = await AppService.getUserApps(axiosPrivate,auth.email)
        console.log(response)
        setUserApps(response)
    }



    return <div className={styles.main_apps}>
        <div className={styles.sidebar_container}>

            <div className={styles.apps_container}>
                {userApps?.map((app) =>
                    <SidebarApp key={app.id} appData={app}/>
                )}
            </div>
            <div className={styles.add_app_container}>
                <img className={styles.add_button} src="/image-13@2x.png" alt="" onClick={() => ref.current.show()} />
            </div>
        </div>
        <div className={styles.app_container}>
            <Routes>
                <Route path="" element={<div className={styles.hello_container}>
                <div className={styles.title_hello}>Multitool problem solver</div>
                    <div className={styles.subtitle_hello}>
                        Find many different applications in one place to better organize your
                        life
                    </div>
                </div>}/>
                <Route path="app/:id" element={<NoteAggregator/>}/>
            </Routes>
        </div>
        <DialogSearch refDialog={ref} data={allApps}/>
    </div>
}

export default Apps;