import { useContext } from "react";
import { Button, Container, Image, Nav, Navbar } from "react-bootstrap";
import { Link, useNavigate } from "react-router-dom";
import { MyUserContext } from "../App";

const Header = () => {
    // const [user, dispatch] = useContext(MyUserContext);
    const nav = useNavigate();

    // const logout = () => {
    //     dispatch({
    //         "type": "logout"
    //     })
    //     nav("/");
    // }

    return (
        <>
            <Navbar bg="dark" data-bs-theme="dark">
                <Container>
                    <Navbar.Brand href="/">News</Navbar.Brand>
                    <Nav className="me-auto">
                        <Link className="nav-link" to="/">Home</Link>
                        <Link className="nav-link" to="/login">Login</Link>
                        {/* <div className="d-flex align-items-center">
                            {
                                user === null ? 
                                <>
                                <Link className="nav-link" to="/register">Register</Link>
                                <Link className="nav-link" to="/login">Login</Link>
                                </> :
                                    <>
                                        <Link className="nav-link" to="/appointments">Appointment</Link>
                                        {user.roleId.name !== "ROLE_PATIENT" &&
                                        <Link className="nav-link" to="/shift">Shift</Link>
                                        }
                                        <Link className="nav-link text-success" to="/">{user.firstName} {user.lastName}</Link>
                                        <Image
                                            src={user.avatar}
                                            roundedCircle
                                            width={30}
                                            height={30}
                                            className="me-2 mt-1"
                                        />
                                        <Button variant="warning" onClick={logout}>Log out</Button>
                                    </>
                            }
                        </div> */}
                    </Nav>

                </Container>
            </Navbar>
        </>)
}

export default Header;