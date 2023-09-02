import Axios from "axios";
import { useContext, useEffect, useState } from "react";
import { Button, FloatingLabel, Form } from "react-bootstrap"
import { useNavigate, useParams } from "react-router-dom";
import { MyUserContext } from "../App";
import axios from "axios";

const Register = () => {
    const [user, dispatch] = useContext(MyUserContext);
    const { id } = useParams();
    const [username, setUsername] = useState(null);
    const [password, setPassword] = useState(null);

    useEffect(() => {
        if (user !== null) {
            setUsername(user.username);
            setPassword(user.password);
        }
    }, []);

    const nav = useNavigate();

    const register = async (evt) => {
        evt.preventDefault();
        let res = await Axios.post("https://localhost:7019/api/User/create-user", {

            "id": 0,
            "username": username,
            "password": password

        });
        console.log(res);
        if (res.status === 200) {
            nav("/login");
        }
        else {
            alert("Đang có lỗi, hãy quay lại sau")
        }

    }

    const updateUser = async (evt) => {
        evt.preventDefault();
        let res = await axios.post("https://localhost:7019/api/User/update-user", {
            "id": user.id,
            "username": username,
            "password": password
        })
        console.log(res);
        if (res.status === 200) {
            nav("/login");
        }
        else {
            alert("Đang có lỗi, hãy quay lại sau")
        }
    }

    return (
        <>
            {
                user === null ? (
                    <>
                        <h1 className="text text-center text-info">Đăng ký</h1>
                        <Form className="container form-control mb-2" onSubmit={register}>
                            <FloatingLabel controlId="username" label="Tài khoản" className="mb-3"
                            >
                                <Form.Control value={username} onChange={e => setUsername(e.target.value)} type="text" placeholder="..." required />
                            </FloatingLabel>
                            <FloatingLabel controlId="password" label="Mật khẩu">
                                <Form.Control value={password} onChange={e => setPassword(e.target.value)} type="password" placeholder="..." required />
                            </FloatingLabel>
                            <Button type="submit" variant="success" className="mt-2" size="lg">Đăng kí</Button>
                        </Form>
                    </>
                ) : (
                    <>
                        <h1 className="text text-center text-info">Cập nhật</h1>
                        <Form className="container form-control mb-2" onSubmit={updateUser}>
                            <FloatingLabel controlId="username" label="Tài khoản mới" className="mb-3"
                            >
                                <Form.Control value={username} onChange={e => setUsername(e.target.value)} type="text" placeholder="..." required />
                            </FloatingLabel>
                            <FloatingLabel controlId="password" label="Mật khẩu mới">
                                <Form.Control value={password} onChange={e => setPassword(e.target.value)} type="password" placeholder="..." required />
                            </FloatingLabel>
                            <Button type="submit" variant="success" className="mt-2" size="lg">Cập nhập</Button>
                        </Form>
                    </>
                )
            }
        </>
    )
}

export default Register;