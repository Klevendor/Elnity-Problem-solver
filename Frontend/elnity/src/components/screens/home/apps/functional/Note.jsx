import { useRef, useState } from "react";
import DialogConfirm from "./DialogConfirm";
import styles from "./Note.module.css"
import {AppNoteService} from "./../../../../services/AppNoteService"
import useAxioPrivate from "./../../../../hooks/useAxiosPrivate"

const BASE_URL = "http://localhost:5223";

const Note = ({isDeletingMode,data,updateData}) =>{

    const ref = useRef()
    const axiosPrivate = useAxioPrivate()

    const [isLoading,setIsLoading] = useState(false)

    const DeleteNote = async () =>{
        setIsLoading(true)
        const response = await AppNoteService.deleteNote(axiosPrivate,data.id)
        if(response)
        {
            updateData()
            ref.current.close()
        }
        setIsLoading(false)
    }

    return <div className={styles.note_container}>
         <div className={styles.content_container}>
            <div className={styles.image_container}>
                <img className={styles.image} src={BASE_URL+data.imagePath} alt="" />
            </div>
            <div className={styles.info_container}>
                <div className={styles.name__container}>
                     <label>Name:</label>
                     <label className={`${styles.my_text}`}>{data.name}</label>
                </div>
                <div className={styles.status_container}>
                    <label>Status:</label>
                    <label className={`${styles.my_text} 
                    ${data.status == "plan" && styles.plan}
                    ${data.status == "procces" && styles.procces}
                    ${data.status == "reviewed" && styles.reviewed}
                    `}>{data.status}</label>
                </div>
                <div className={styles.current_state_container}>
                    <label>Current state:</label>
                    <label className={`${styles.my_text}`}>{data.currentState}</label>
                </div>
                <div className={styles.note_text_container}>
                    <label>Note:</label>
                    <label className={`${styles.my_text}`}>{data.note}</label>
                </div>
            </div>
        </div>
        <div className={styles.close_container}>
            <i className={`fa-regular fa-circle-xmark ${styles.close} ${isDeletingMode?"":styles.close_none}`} onClick={()=> ref.current.show()}></i>
        </div>
        <DialogConfirm refDialog={ref} funcAfterConfirm={DeleteNote} text={"Are you sure you want to DELETE this field?" } isLoadingConfirm={isLoading}/>
  </div>
}

export default Note;