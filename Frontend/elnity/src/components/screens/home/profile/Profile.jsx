import { useState, useEffect } from "react";
import styles from "./Profile.module.css";
import { useNavigate } from "react-router-dom";
import {ValidationService} from "../../../services/ValidationService"
import { UserService } from "../../../services/UserService";
import { axiosPrivate } from "../../../api/Axios";
import useAxiosPrivate from "../../../hooks/useAxiosPrivate";
import useAuth from "../../../hooks/useAuth";


const Profile = ({data,updateData}) => {
    const navigate = useNavigate()
    const {auth} = useAuth()
    const axiosPrivate = useAxiosPrivate()
    const [fileSelected, setFileSelected] = useState();
    const [preview, setPreview] = useState()
    
    const [username,setUsername] = useState("")
    const [fullname,setFullName] = useState("")
    const [number,setNumber] = useState("")
    const [date,setDate] = useState("")
    const [imagePath,setImagePath] = useState("/default-avatar.png")

    const saveFileSelected = (e) => {
        setFileSelected(e.target.files[0]);
    };

    useEffect(()=>{
        setUsername(data?.username)
        setFullName(data?.fullName?data.fullName : "" )
        setNumber(data?.number?data.number : "" )
        setDate(data?.birthday?new Date(data?.birthday).toISOString().split('T')[0]:"")
        setImagePath(data?.avatarPath)
    },[])

    useEffect(() => {
        if (!fileSelected) {
            setPreview(undefined)
            return
        }

        const objectUrl = URL.createObjectURL(fileSelected)
        setPreview(objectUrl)

        return () => URL.revokeObjectURL(objectUrl)
    }, [fileSelected])

    const SaveChanges = async () =>{
        console.log([username,fullname,number,date])
        const resUsername = await ValidationService.validateUserName(username)
        if(!resUsername || username=="Username must have only letters, disgits, _")
        {
            setUsername("Username must have only letters, disgits, _")
            return
        }
        const resNumber= await ValidationService.validateNumber(number)
        console.log(resNumber)
        if(!resNumber || number=="Number must have 12 digits wit region")
        {
            setNumber("Number must have 12 digits wit region")
            return
        }   
        const data = {
            email:auth.email,
            username:username,
            fullname:fullname,
            number:number,
            birthday:date,
            image:fileSelected
        }
        const response = await UserService.changeUserInfo(axiosPrivate,data)
        if(response)
            updateData()
    }

    return <div className={styles.profile}>
        <div className={styles.profile_container}>
            <div className={styles.return_container}>
                <button className={styles.return_button} onClick={()=>navigate("/home")}>Home</button>
            </div>
            <div className={styles.form_container}>
                <div className={styles.avatar_container}>
                    <img className={styles.avatar_full} alt="" src={!fileSelected ? imagePath : preview} />
                    <div className={styles.upload_button_container}>
                        <label className={styles.upload_button}>
                            <input className={styles.upload_image} type="file" accept=".jpg, .png" onChange={saveFileSelected} hidden />
                            Change image
                        </label>
                    </div>
                </div>
                <div className={styles.fields_container}>
                    <div className={styles.field_container}>
                        <p className={styles.field_title}>Username</p>
                        <input type="text" className={styles.field_input} 
                        value={username} onChange={(e)=>setUsername(e.target.value)}/>
                    </div>
                    <div className={styles.field_container}>
                        <p className={styles.field_title}>Full Name</p>
                        <input type="text" className={styles.field_input} 
                        value={fullname} onChange={(e)=>setFullName(e.target.value)}/>
                    </div>
                    <div className={styles.field_container}>
                        <p className={styles.field_title}>Number</p>
                        <input type="text" className={styles.field_input} 
                        value={number} onChange={(e)=>setNumber(e.target.value)}/>
                    </div>
                    <div className={styles.field_container}>
                        <p className={styles.field_title}>Birthday</p>
                        <input type="date" className={styles.field_input} 
                        value={date} onChange={(e)=>setDate(e.target.value)}/>
                    </div>
                </div>
            </div>
        </div>
        <div className={styles.save_container}>
            <button className={styles.save_button} onClick={SaveChanges}>Save changes</button>
        </div>
    </div>
}

export default Profile;