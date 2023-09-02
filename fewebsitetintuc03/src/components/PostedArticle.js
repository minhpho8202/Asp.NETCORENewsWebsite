import { useContext, useEffect, useState } from "react";
import { MyUserContext } from "../App";
import axios from "axios";
import { Button, Container, Table } from "react-bootstrap";
import { useNavigate } from "react-router-dom";

const PostedArticle = () => {
    const [user, dispatch] = useContext(MyUserContext);
    const [articles, setArticles] = useState([])
    const nav = useNavigate();

    const loadArticles = async () => {
        try {
            const response = await axios.get(`https://localhost:7019/api/Article/get-article-by-userId?userId=${user.id}`);
            setArticles(response.data.data);
            console.log(response.data.data);
        } catch (error) {
            console.error('failed', error);
        }
    };

    const handleView = (id) => {
        nav(`/articles/${id}`);
    }

    const handleUpdateArticle = (id) => {
        nav(`/update-article/${id}`);
    }

    const handleDeleleArticle = async (id) => {
        try {
            const response = await axios.delete(`https://localhost:7019/api/Article/article?articleId=${id}`);
            console.log(response.data);
            if (response.data === id) {
                alert("Xóa thành công");
                loadArticles();
            }
            else {
                alert("Thất bại")
            }
        } catch (error) {
            console.error('failed', error);
        }
    }

    useEffect(() => {
        loadArticles();
    }, [])

    return (
        <Container>
            <h1 className="text text-center text-info" >Bài viết đã đăng</h1>
            <Table striped bordered hover>
                <thead>
                    <tr>
                        <th>title</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {articles && articles.map((article) => (
                        <tr key={article.id}>
                            <td>{article.title}</td>
                            <td>
                            <Button variant="success" className="btn" onClick={() => handleView(article.id)} >Xem</Button>
                            <Button variant="warning" className="btn ms-2" onClick={() => handleUpdateArticle(article.id)} >Sửa</Button>
                            <Button variant="danger" className="btn ms-2" onClick={() => handleDeleleArticle(article.id)} >Xóa</Button>
                            </td>
                        </tr>
                    ))}
                </tbody>

            </Table>
        </Container>
    )
}

export default PostedArticle;