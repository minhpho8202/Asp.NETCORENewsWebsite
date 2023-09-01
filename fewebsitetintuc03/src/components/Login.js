import { useContext, useState } from "react";
import { Button, FloatingLabel, Form } from "react-bootstrap";
import { MyUserContext } from "../App";
import cookie from "react-cookies";
import { Navigate } from "react-router-dom";

const Login = () => {
    const [user, dispatch] = useContext(MyUserContext);
    const [username, setUsername] = useState(null);
    const [password, setPassword] = useState(null);

    const login = (evt) => {
        evt.preventDefault();

        if (username !== null && password !== null) {
            const process = async () => {
                try {
                    let res = await Apis.post(endpoints['login'], {
                        "username": username,
                        "password": password
                    });
                    cookie.save("user", data);

                    console.info(data);
                    dispatch({
                        "type": "login",
                        "payload": data
                    })
                } catch (error) {
                    console.error("failed", error);
                }
            }

            process();
        } else {
            alert("please enter username and password");
        }

    }

    if (user !== null) {
        return <Navigate to="/" />
    }
    return (
        <>
            <h1 className="text text-center text-info" >Login</h1>
            <Form className="container form-control mb-2" onSubmit={login}>
                <FloatingLabel controlId="username" label="Tài khoản" className="mb-3"
                >
                    <Form.Control value={username} onChange={e => setUsername(e.target.value)} type="text" placeholder="..." />
                </FloatingLabel>
                <FloatingLabel controlId="password" label="Mật khẩu">
                    <Form.Control value={password} onChange={e => setPassword(e.target.value)} type="password" placeholder="..." />
                </FloatingLabel>
                <Button type="submit" variant="success" className="mt-2" size="lg">Login</Button>
            </Form>
        </>
    )
}

export default Login;