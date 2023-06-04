import { useRef } from "react";
import styles from "./Apps.module.css";
import DialogSearch from "./DialogSearch"

const Apps = () => {
    const ref = useRef()

    const OpenSearch = () => {
        ref.current.show()
    }

    return <div className={styles.main_apps}>
        <div className={styles.sidebar_container}>

            <div className={styles.apps_container}>
                <div className={styles.app}>
                    fd
                </div>
                <div className={styles.app}>
                    fd
                </div>
                <div className={styles.app}>
                    fd
                </div>
                <div className={styles.app}>
                    fd
                </div>
                <div className={styles.app}>
                    fd
                </div>
                <div className={styles.app}>
                    fd
                </div>
                <div className={styles.app}>
                    fd
                </div>
                <div className={styles.app}>
                    fd
                </div>
            </div>
            <div className={styles.add_app_container}>
                <img className={styles.add_button} src="/image-13@2x.png" alt="" onClick={() => ref.current.show()} />
            </div>
        </div>
        <div className={styles.app_container}>

        </div>
       <DialogSearch refDialog={ref}/>
    </div>
}

export default Apps;