import { useState } from "react";
import styles from "./DialogAddNote.module.css"
import { useEffect } from "react";
import { AppNoteService } from "../../../../services/AppNoteService";
import useAuth from "./../../../../hooks/useAuth";
import useAxiosPrivate from "../../../../hooks/useAxiosPrivate"

const DialogAddNote = ({refDialog,updateData}) =>{
    const {auth} = useAuth()
    const axiosPrivate = useAxiosPrivate()

    const [fileSelected, setFileSelected] = useState();
    const [preview, setPreview] = useState()

    const [name,setName] = useState("")
    const [current,setCurrent] = useState("")
    const [status,setStatus] = useState("reviewed")
    const [note,setNote] = useState("")

    useEffect(() => {
        if (!fileSelected) {
            setPreview(undefined)
            return
        }
        const objectUrl = URL.createObjectURL(fileSelected)
        setPreview(objectUrl)

        return () => URL.revokeObjectURL(objectUrl)
    }, [fileSelected])

    const saveFileSelected = (e) => {
        setFileSelected(e.target.files[0]);
    };

    const Add = async () => {
        if(name=="")
        {
            setName("Field cannot be empty")
            return
        }
        if(!fileSelected)
        {
            console.log("Empty image")
            return
        }

        const data ={
            email:auth.email,
            name:name,
            status:status,
            current:current,
            note:note,
            image:fileSelected
        }

        const response = await AppNoteService.addNote(axiosPrivate,data)
        if(response)
        {
            console.log("all ok")
            updateData()
            refDialog.current.close()
        }
        setName("")
        setCurrent("")
        setStatus("reviewed")
        setNote("")
        setFileSelected()
    }

    return <dialog ref={refDialog} className={styles.modal}>
    <div className={styles.center}>
        <div className={styles.modal_container}>
            <div className={styles.control_panel}>
                <i className={`fa-regular fa-circle-xmark ${styles.close}`} onClick={() => refDialog.current.close()}></i>
            </div>
            <div className={styles.form_container}>
                <div className={styles.image_container}>
                    <img className={styles.image}  src={!fileSelected ? "/Default_add.png" : preview} alt="" />
                    <label className={styles.upload_button}>
                            <input type="file" accept=".jpg, .png" onChange={saveFileSelected}  hidden />
                            Change image
                        </label>
                </div>
                <div className={styles.fields_container}>
                    <div>
                        <label>Name: </label>
                        <input className={styles.input} type="text"
                        value={name} onChange={(e)=>setName(e.target.value)}/>
                    </div>
                    <div>
                        <label>Status: </label>
                        <select className={styles.select} value={status} onChange={(e)=>setStatus(e.target.value)}>
                            <option value="reviewed">Reviewed</option>
                            <option value="plan">In plan</option>
                            <option value="procces">In process</option>
                        </select>
                    </div>
                    <div className={styles.current_state}>
                        <label>Current state: </label>
                        <input className={styles.input_current} type="text"
                         value={current} onChange={(e)=>setCurrent(e.target.value)}/>
                    </div>
                    <div className={styles.note_container}>
                        <label>Note: </label>
                        <textarea className={styles.note} 
                         value={note} onChange={(e)=>setNote(e.target.value)}
                         ></textarea>
                    </div>
                </div>
            </div>
            <div className={styles.action_container}>
                    <button className={styles.button_add} onClick={Add}>Add</button>
                </div>
        </div>
    </div>
</dialog>
}

export default DialogAddNote;