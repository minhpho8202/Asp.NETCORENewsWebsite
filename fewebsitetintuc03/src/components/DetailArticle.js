import { useContext, useEffect, useState } from 'react';
import { Link, useParams } from 'react-router-dom';
import axios from "axios";
import { Button, Card, Container, Form } from 'react-bootstrap';
import { MyUserContext } from '../App';

const DetailArticle = () => {
    const [user, dispatch] = useContext(MyUserContext);
    const { id } = useParams();
    const [article, setArticle] = useState();
    const [comment, setComment] = useState("");
    const [liked, setLiked] = useState(false);
    const [isEditing, setIsEditing] = useState(false);
    const [editedComment, setEditedComment] = useState("");

    const loadArticle = async () => {
        try {
            const response = await axios.get(`https://localhost:7019/api/Article/get-article-by-id/${id}`);
            setArticle(response.data.data);
            console.log(response.data.data);
            const isLiked = await axios.get(`https://localhost:7019/api/Like/isLiked?articleId=${id}&userId=${user.id}`);
            if (isLiked.data === true) {
                setLiked(true)
            }
            console.log(isLiked.data);
        } catch (error) {
            console.error('failed', error);
        }
    }

    const handleLike = async () => {
        if (liked === true) {
            const res = await axios.delete(`https://localhost:7019/api/Like/like?articleId=${id}&userId=${user.id}`)
            console.log(res.data);
            if (res.data > 0) {
                setLiked(false);
            }
        } else {
            const res = await axios.post("https://localhost:7019/api/Like/create-like", {
                "id": 0,
                "userId": user.id,
                "articleId": id
            })
            console.log(res.status)
            if(res.status === 200) {
                setLiked(true);
            }
        }
    }

    const handleComment = async () => {
        if (comment !== "") {
            const response = await axios.post("https://localhost:7019/api/Comment/create-comment", {
                "id": 0,
                "userId": user.id,
                "articleId": id,
                "content": comment
            });
            if (response.status === 200) {
                loadArticle();
            } else {
                article("Lỗi");
            }
        } else {
            alert("Hãy nhập nội dung");
        }
        setComment("");
    }

    const handleEditComment = async (id) => {
        if (editedComment !== "") {
            const response = await axios.post("https://localhost:7019/api/Comment/update-comment", {
                "id": id,
                "content": editedComment
            });
            if (response.status === 200) {
                loadArticle();
                setIsEditing(false);
            } else {
                article("Lỗi");
            }
        } else {
            alert("Hãy nhập nội dung");
        }
        setComment("");
    }

    const handleDeleteComment = async (id) => {
        try {
            const response = await axios.delete(`https://localhost:7019/api/Comment/comment/${id}`);
            console.log(response.data);
            if (response.data === id) {
                alert("Xóa thành công");
                loadArticle();
            }
            else {
                alert("Thất bại")
            }
        } catch (error) {
            console.error('failed', error);
        }
    }

    useEffect(() => {
        loadArticle();
    }, []);

    const renderHTML = (htmlString) => {
        return { __html: htmlString };
    };

    return (
        <Container>
            {article && (
                <>
                    <h1 className="text-center text-success">{article.title}</h1>
                    <Card>
                        <Card>
                            <Card.Body>
                                <Card.Text className="text-muted">
                                    Tác giả: {article.user ? article.user.username : 'Không có tác giả'}
                                    &nbsp; - &nbsp;
                                    Ngày đăng: {article.createdDate ? article.createdDate : 'Không có ngày đăng'}
                                </Card.Text>
                            </Card.Body>
                        </Card>
                    </Card>
                    <div dangerouslySetInnerHTML={renderHTML(article.content)} />
                    {article.comments && article.comments.length > 0 && (
                        <div className="article-comments mt-4">
                            <p className="comment-count">Số lượt bình luận: {article.comments.length}</p>
                            <p className="like-count">Số lượt thích: {article.likes ? article.likes.length : 0}</p>
                        </div>
                    )}
                    {
                        user === null ?
                            <>
                                <Link to="/login">Hãy đăng nhập để thích bài viết này</Link>
                            </> :
                            <>
                                <Button
                                    variant="success"
                                    className="mr-3 mb-2"
                                    onClick={handleLike}
                                >
                                    {liked ? 'Bỏ thích' : 'Thích'}
                                </Button>
                            </>
                    }

                    <Form>
                        <Form.Group>
                            <Form.Control
                                type="text"
                                placeholder="Nhập bình luận của bạn..."
                                value={comment}
                                onChange={(e) => setComment(e.target.value)}
                                required
                            />
                        </Form.Group>
                        {
                            user === null ?
                                <>
                                    <Link to="/login">Hãy đăng nhập để bình luận bài viết này</Link>
                                </> :
                                <>
                                    <Button variant="primary" className="mt-2 mb-2" onClick={handleComment}>Bình luận</Button>
                                </>
                        }
                    </Form>
                    {article.comments && article.comments.length > 0 && (
                        <div className="comment-list mt-4">
                            <h3>Danh sách bình luận:</h3>
                            <ul>
                                {article.comments.map((comment) => (
                                    <li key={comment.id} className="comment-item">
                                        <p className="comment-user">{comment.user ? comment.user.username : 'Không có tác giả'}</p>
                                        <p className="comment-date">{comment.createdDate ? comment.createdDate : 'Không có ngày đăng'}</p>
                                        <p className="comment-date">{comment.content ? comment.content : 'Không có nội dung'}</p>
                                        {user !== null && user.id === comment.userId && (
                                            <>
                                                <Button className="btn btn-warning me-2" onClick={() => setIsEditing(!isEditing)}>Chỉnh sửa</Button>
                                                <Button className="btn btn-danger" onClick={() => handleDeleteComment(comment.id)}>Xóa</Button>
                                            </>
                                        )}
                                        {
                                            isEditing && user.id === comment.userId && <>
                                                <Form className="mt-2">
                                                    <Form.Group>
                                                        <Form.Control
                                                            type="text"
                                                            placeholder="Nhập bình luận mới của bạn..."
                                                            value={editedComment}
                                                            onChange={(e) => setEditedComment(e.target.value)}
                                                            required
                                                        />
                                                    </Form.Group>
                                                    <Button variant="primary" className="mt-2 mb-2" onClick={() => handleEditComment(comment.id)}>Sửa</Button>
                                                </Form>
                                            </>
                                        }
                                    </li>
                                ))}
                            </ul>
                        </div>
                    )}
                </>
            )}
        </Container>
    );
};

export default DetailArticle;