import { useEffect, useState } from "react";
import AppSearch from "./AppSearch";
import styles from "./DialogSearch.module.css";

const DialogSearch = ({refDialog,data}) =>{
    
    const [appsData, setAppsData] = useState([])
    const [filterName,setFilterName] = useState("")
    const [filteredData,setFilteredData] = useState([])

    useEffect(() => {
        if (filterName === "") {
            setFilteredData(appsData)
        }
        else {
            setFilteredData(appsData.filter((app) => { return app.name.indexOf(filterName) !== -1 }))
        }
    }, [filterName])

    useEffect(()=>{
        setAppsData(data)
        setFilteredData(data)
    },[data])

    return  <dialog ref={refDialog} className={styles.modal}>
    <div className={styles.center}>
        <div className={styles.modal_container}>
            <div className={styles.control_panel}>
                <i className={`fa-regular fa-circle-xmark ${styles.close}`} onClick={() => refDialog.current.close()}></i>
            </div>
            <div className={styles.search_panel}>
                <div className={styles.search_container}>
                    <input type="text" name="" id="" className={styles.search}
                    value={filterName} onChange={(e)=> setFilterName(e.target.value)}/>
                    <div className={styles.loop}>
                        <i className="fa-solid fa-magnifying-glass"></i>
                    </div>
                </div>
            </div>
            {filteredData?.length == 0
                    ? <div className={styles.app_not_found}>
                        No application found
                        </div>
                    : <div className={styles.search_apps_container}>
                    {
                        filteredData?.map((app) => <AppSearch key={app.id} appData={app}/>)
                    }
                    </div>
            }
        </div>
    </div>
</dialog>
}

export default DialogSearch;