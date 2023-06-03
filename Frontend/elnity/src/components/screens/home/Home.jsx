import styles from "./Home.module.css";
import Apps from "./apps/Apps";
import Header from "./header/Header"

const Home = () => {
    return <div className={styles.main_page}>
        <div className={styles.overlay_bg_colors}>
            <Header/>
            <Apps/>
        </div>
    </div>
}

export default Home;