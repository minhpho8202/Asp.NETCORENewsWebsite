import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import axios from "axios";
import { Button, Card, Container, Form } from 'react-bootstrap';

const DetailArticle = () => {
    const { id } = useParams();
    const [article, setArticle] = useState();
    const [comment, setComment] = useState('');
    const [liked, setLiked] = useState(false);

    const loadArticle = async () => {
        try {
            const response = await axios.get(`https://localhost:7019/api/Article/get-article-by-id/${id}`);
            setArticle(response.data.data);
            console.log(response.data.data);
        } catch (error) {
            console.error('failed', error);
        }
    }

    const handleLike = async () => {
        // Gửi yêu cầu API để thực hiện hành động "Thích" bài viết
        // Tùy theo cấu trúc của API của bạn
        // Ví dụ: await axios.post(`https://your-api.com/like-article/${id}`);
        // Sau khi thành công, bạn có thể cập nhật state liked.
        setLiked(!liked);
    }

    const handleComment = async () => {
        // Gửi yêu cầu API để thêm bình luận cho bài viết
        // Tùy theo cấu trúc của API của bạn
        // Ví dụ: await axios.post(`https://your-api.com/comment-article/${id}`, { content: comment });
        // Sau khi thành công, bạn có thể làm mới trang hoặc cập nhật danh sách bình luận.
        // Ở đây, chúng tôi không làm điều này để đơn giản hóa ví dụ.
        setComment('');
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
                    <Button
                        variant="success"
                        className="mr-3 mb-2"
                        onClick={handleLike}
                    >
                        {liked ? 'Bỏ thích' : 'Thích'}
                    </Button>

                    {/* Form bình luận */}
                    <Form>
                        <Form.Group>
                            <Form.Control
                                type="text"
                                placeholder="Nhập bình luận của bạn..."
                                value={comment}
                                onChange={(e) => setComment(e.target.value)}
                            />
                        </Form.Group>
                        <Button variant="primary" className="mt-2 mb-2" onClick={handleComment}>Bình luận</Button>
                    </Form>
                    {article.comments && article.comments.length > 0 && (
                        <div className="comment-list mt-4">
                            <h3>Danh sách bình luận:</h3>
                            <ul>
                                {article.comments.map((comment) => (
                                    <li key={comment.id} className="comment-item">
                                        <p className="comment-user">{comment.user ? comment.user.username : 'Không có tác giả'}</p>
                                        <p className="comment-date">{comment.createdDate ? comment.createdDate : 'Không có ngày đăng'}</p>
                                        <p className="comment-content" dangerouslySetInnerHTML={{ __html: comment.content }} />
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