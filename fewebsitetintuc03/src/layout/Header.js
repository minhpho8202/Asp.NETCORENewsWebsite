import { useContext } from "react";
import { Button, Container, Image, Nav, Navbar } from "react-bootstrap";
import { Link, useNavigate } from "react-router-dom";
import { MyUserContext } from "../App";

const Header = () => {
    const [user, dispatch] = useContext(MyUserContext);
    const nav = useNavigate();

    const logout = () => {
        dispatch({
            "type": "logout"
        })
        nav("/");
    }

    return (
        <>
            <Navbar bg="dark" data-bs-theme="dark">
                <Container>
                    <Navbar.Brand href="/">Tin tức</Navbar.Brand>
                    <Nav className="me-auto">
                        <Link className="nav-link" to="/">Trang chủ</Link>
                        <Link className="nav-link" to="/country">Thông tin quốc gia</Link>
                        <div className="d-flex align-items-center">
                            {
                                user === null ? 
                                <>
                                <Link className="nav-link" to="/register">Đăng kí</Link>
                                <Link className="nav-link" to="/login">Đăng nhập</Link>
                                </> :
                                    <>
                                        {user.role !== "USER" &&
                                        <Link className="nav-link" to="/stats">Thống kê</Link>
                                        }
                                        <Link className="nav-link" to="/posted-article">Đã đăng</Link>
                                        <Link className="nav-link" to="/add-article">Thêm bài viết</Link>
                                        <Link className="nav-link text-success" to={`/update-user/${user.id}`}>{user.username}</Link>
                                        <Button variant="warning" onClick={logout}>Log out</Button>
                                    </>
                            }
                        </div>
                    </Nav>

                </Container>
            </Navbar>
        </>)
}

export default Header;