import styles from "./DialogConfirm.module.css"

const DialogConfirm = ({refDialog,funcAfterConfirm,text,isLoadingConfirm}) => {

    return <dialog ref={refDialog} className={styles.modal}>
                <div className={styles.center}>
                    <div className={styles.modal_container}>
                        <div className={styles.control_panel}>
                                <p className={styles.title}>Confirmation</p>
                                <i className={`fa-regular fa-circle-xmark ${styles.close}`} onClick={() => refDialog.current.close()}></i>
                            </div>
                        <div className={styles.message_panel}>
                            <p className={styles.message}>{text}</p>
                        </div>
                        <div className={styles.action_panel}>
                            {
                                isLoadingConfirm
                                ? <div className={styles.loader}></div>
                                : <>
                                 <button className={styles.yes_button} onClick={funcAfterConfirm}>Yes</button>
                            <button className={styles.no_button} onClick={() => refDialog.current.close()}>No</button>
                            </>
                            }
                        </div>
                    </div>
                </div>
            </dialog>
};

export default DialogConfirm;
