import { useEffect, useRef, useState } from "react";
import useAuth from "../../../../hooks/useAuth";
import Note from "./Note";
import styles from "./NoteAggregator.module.css"
import DialogAddNote from "./DialogAddNote";
import { AppNoteService } from "../../../../services/AppNoteService";
import useAxiosPrivate from "../../../../hooks/useAxiosPrivate";



const NoteAggregator = () =>{
    const ref = useRef()
    const {auth} = useAuth()
    const axioPrivate = useAxiosPrivate()

    const [isDeletingMode,setIsDeletingMode] = useState(false)
    const [notes,setNotes] = useState()
    const [filterName,setFilterName] = useState("")
    const [filteredData,setFilteredData] = useState([])

    useEffect(() => {
        if (filterName === "") {
            setFilteredData(notes)
        }
        else {
            setFilteredData(notes.filter((note) => { return note.name.indexOf(filterName) !== -1 }))
        }
    }, [filterName])

    useEffect(()=>{
        fetchData()
    },[])

    const fetchData = async () =>{
        const response = await AppNoteService.getNotesUser(axioPrivate,auth.email)
        console.log(response)
        setNotes(response)
        setFilteredData(response)
    }

    return <div className={styles.app_note_aggregator_container}>
        <div className={styles.functionl_container}>
            <div className={styles.input_container}>
                <input className={styles.input_search} type="text" placeholder="Search by name"
                value={filterName} onChange={(e)=>setFilterName(e.target.value)}/>
                <div className={styles.loop}>
                        <i className="fa-solid fa-magnifying-glass"></i>
                    </div>
            </div>
            <div className={styles.buttons_container}>
                <button className={styles.button_add} onClick={()=>ref.current.show()}>Add</button>
                <button className={`${isDeletingMode? styles.button_done: styles.button_delete}`} onClick={()=>setIsDeletingMode(!isDeletingMode)}>{isDeletingMode?"Done":"Delete"}</button>
            </div>
        </div>
        <div className={styles.notes_container}>
            {filteredData?.length ==0
            ? <div className={styles.empty}>
                Empty
            </div>
            : filteredData?.map((note) => 
                <Note isDeletingMode={isDeletingMode} key={note.id} data={note} updateData={fetchData}/>
            )
             }
           
        </div>
        <DialogAddNote refDialog={ref} updateData={fetchData}/>
    </div>
}

export default NoteAggregator;