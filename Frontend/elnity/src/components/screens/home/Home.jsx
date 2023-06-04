import { Route, Routes } from "react-router-dom";
import styles from "./Home.module.css";
import Apps from "./apps/Apps";
import Header from "./header/Header"
import Profile from "./profile/Profile";
import useAxiosPrivate from "../../hooks/useAxiosPrivate";
import useAuth from "../../hooks/useAuth";
import { UserService } from "../../services/UserService";
import { useEffect, useState } from "react";

const BASE_URL = "http://localhost:5223";

const Home = () => {
    const { auth, setAuth } = useAuth()
    const axiosPrivate = useAxiosPrivate()
    const [userData, setUserData] = useState()
    const [isLoading, setIsLoading] = useState(false)


    useEffect(() => {
        fetchData()
    }, [])

    const fetchData = async () => {
        setIsLoading(true)
        const response = await UserService.getUserInfo(axiosPrivate, auth.email)
        response.avatarPath = BASE_URL+response.avatarPath 
        setUserData(response)
        console.log(response)
        setIsLoading(false)
    }


    return isLoading ? <div className={styles.loader_container}><div className={styles.big_loader}></div></div>
        : <div className={styles.main_page}>
            <div className={styles.overlay_bg_colors}>
                <Header data={userData} />
                <Routes>
                    <Route path="" element={<Apps />} />
                    <Route path="profile" element={<Profile data={userData} updateData={fetchData} />} />
                </Routes>
            </div>
        </div>
}

export default Home;