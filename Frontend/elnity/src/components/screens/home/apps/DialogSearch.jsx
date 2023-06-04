import styles from "./DialogSearch.module.css";

const DialogSearch = ({refDialog}) =>{

    return  <dialog ref={refDialog} className={styles.modal}>
    <div className={styles.center}>
        <div className={styles.modal_container}>
            <div className={styles.control_panel}>
                <i className={`fa-regular fa-circle-xmark ${styles.close}`} onClick={() => refDialog.current.close()}></i>
            </div>
            <div className={styles.search_panel}>
                <div className={styles.search_container}>
                    <input type="text" name="" id="" className={styles.search}/>
                    <div className={styles.loop}>
                        <i className="fa-solid fa-magnifying-glass"></i>
                    </div>
                </div>
            </div>
            <div className={styles.search_apps_container}>
                <div className={styles.app_with_label_container}>
                    <div className={styles.app2}>
                    fd
                    </div>
                    <p className={styles.app_title}>test</p>
                </div>
               
                
                    
                </div>
        </div>
    </div>
</dialog>
}

export default DialogSearch;