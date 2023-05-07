import './main.css'
import { useRef } from 'react';
import {useNavigate} from "react-router-dom"

const Main = () => {
    const navigate = useNavigate();

    const refSector1 = useRef()
    const refSector2 = useRef()
    const refSector3 = useRef()

    const handleLoginClick = () => {
        navigate("/login");
    }

    const handleRegisterClick = () => {
        navigate("/register");
    }


    const handleAboutClick = () => {
        refSector1.current.scrollIntoView({ block: "start", behavior: "smooth" })
    }

    const handlePosibilityClick = () => {
        refSector2.current.scrollIntoView({ block: "start", behavior: "smooth" })
    }

    const handleContactsClick = () => {
        refSector3.current.scrollIntoView({ block: "start", behavior: "smooth" })
    }

    return <div className="first">
        <div className="header">
            <div className="div">
                <div className="div1">
                    <div className="div2">
                        <div className="div3">
                            <div className="div4">
                                <div className="a">
                                    <div className="a1" id="aContainer">
                                        <div className="about-it" onClick={handleAboutClick}>About it</div>
                                    </div>
                                    <div className="possibility" id="possibilityText" onClick={handlePosibilityClick}>
                                        Possibility
                                    </div>
                                </div>
                                <div className="a2">
                                    <div className="pseudo"></div>
                                    <div className="contacts" id="contactsText" onClick={handleContactsClick}>Contacts</div>
                                </div>
                            </div>
                        </div>
                        <div className="div5">
                            <div className="div6">
                                <div className="a3" id="aContainer3">
                                    <div className="sign-in" onClick={handleLoginClick}>Sign in</div>
                                </div>
                                <div className="sing-up" id="singUpText" onClick={handleRegisterClick}>Sing up</div>
                            </div>
                        </div>
                    </div>
                </div>
                <img className="logo-icon" alt="" src="./public/logo.svg" />
            </div>
            <div className="e-l-n">E l n i t y</div>
            <div className="div7"></div>
            <div className="div8"></div>
        </div>
        <div className="main">
            <div className="divteam23-hero">
                <div className="div9">
                    <div className="div10">
                        <div className="white-center-small">
                            <div className="white-center-small1">
                                <img className="vector-icon" alt="" src="./public/vector.svg" />
                                <div className="e-l-n1">E l n i t y</div>
                                <img className="logo-icon1" alt="" src="./public/logo1.svg" />
                                <div className="multitool-problem-solver">
                                    Multitool problem solver
                                </div>
                            </div>
                        </div>
                        <div className="div11">
                            Find many different applications in one place to better
                            organize your life
                        </div>
                    </div>
                    <div className="div-child" id="rectangle" onClick={handleRegisterClick}>Register now</div>
                </div>
            </div>
            <div className="divhero" ref={refSector1} data-scroll-to="divheroContainer">
                <div className="div12">
                    <div className="h1">
                        <div className="its-simpe">It's simpe</div>
                        <div className="and-saves-your-container">
                            <span className="and-saves-your-container1"
                            ><p className="and-saves-your">and saves your</p>
                                <p className="and-saves-your">time</p></span
                            >
                        </div>
                    </div>
                </div>
                <img className="image-17-icon" alt="" src="./public/image-17@2x.png" />
            </div>
            <div className="div13" ref={refSector2} data-scroll-to="divContainer1">
                <div className="div14">
                    <div className="elnity-problem-solver">
                        Elnity problem solver are designed for all types of work
                    </div>
                    <div className="ul">
                        <div className="a4"><b className="films">Films</b></div>
                        <div className="a5"><div className="books">Books</div></div>
                        <div className="a6"><div className="books">Musics</div></div>
                        <div className="a7"><div className="books">Training</div></div>
                    </div>
                </div>
                <img
                    className="mask-group-icon"
                    alt=""
                    src="./public/mask-group@2x.png"
                /><img
                    className="image-18-icon"
                    alt=""
                    src="./public/image-18@2x.png"
                /><img className="image-19-icon" alt="" src="./public/image-19@2x.png" />

                <div className="div15"></div>
                <div className="foot">
                    <div className="div19"ref={refSector3} data-scroll-to="divContainer">
                        <div className="privacy-policy">Privacy policy</div>
                        <div className="terms">Terms</div>
                        <div className="modern-slavery-act">Modern Slavery Act</div>
                        <div className="impressum">Impressum</div>
                        <div className="copyright-2023">Copyright © 2023 Elnity</div>
                        <div className="ul1">
                            <img className="a-icon" alt="" src="./public/a.svg" /><img className="a-icon" alt="" src="./public/a1.svg" />
                            <img className="a-icon2" alt="" src="./public/a2.svg" /><img className="a-icon" alt="" src="./public/a3.svg" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

}

export default Main