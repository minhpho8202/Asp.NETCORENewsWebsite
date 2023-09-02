import axios from "axios";
import { useState } from "react";
import { Button, Container, Form } from "react-bootstrap";

const Stats = () => {
    const [startDate, setStartDate] = useState("");
    const [endDate, setEndDate] = useState("");
    const [commentCount, setCommentCount] = useState(0);
    const [userCount, setUserCount] = useState(0);
    const [articleCount, setArticleCount] = useState(0);

    const handleSubmit = async (e) => {
        e.preventDefault();

        try {
            const articleResponse = await axios.get(`https://localhost:7019/api/Stats/count-article?startDate=${startDate}&endDate=${endDate}`);
            const userResponse = await axios.get(`https://localhost:7019/api/Stats/count-user?startDate=${startDate}&endDate=${endDate}`);
            const commentResponse = await axios.get(`https://localhost:7019/api/Stats/count-comment?startDate=${startDate}&endDate=${endDate}`);

            if (articleResponse.status === 200 && userResponse.status === 200 && commentResponse.status === 200) {
                setArticleCount(articleResponse.data);
                setUserCount(userResponse.data);
                setCommentCount(commentResponse.data);
            } else {
                setArticleCount("");
                setUserCount("");
                setCommentCount("");
                alert('Đã xảy ra lỗi khi lấy dữ liệu.');
            }
            console.log(articleResponse.data);
            console.log(userResponse.data);
            console.log(commentResponse.data);
        } catch (error) {
            console.error('Lỗi:', error);
            setArticleCount("");
            setUserCount("");
            setCommentCount("");
            alert('Đã xảy ra lỗi khi thực hiện yêu cầu.');
        }
    }

    return (
        <Container>
            <h1 className="text text-center text-info">Thống kê</h1>
            <Form onSubmit={handleSubmit} className="mb-3 mt-2">
                <Form.Group>
                    <Form.Label>Ngày bắt đầu</Form.Label>
                    <Form.Control
                        type="date"
                        value={startDate}
                        onChange={(e) => setStartDate(e.target.value)}
                    />
                </Form.Group>
                <Form.Group>
                    <Form.Label>Ngày kết thúc</Form.Label>
                    <Form.Control
                        type="date"
                        value={endDate}
                        onChange={(e) => setEndDate(e.target.value)}
                    />
                </Form.Group>
                <Button variant="primary" type="submit" className="mt-2">
                    Xem thống kê
                </Button>
            </Form>
            {commentCount !== null && userCount !== null && articleCount !== null && (
                <div className="mt-3">
                    <p>Số lượng bài viết trong khoảng thời gian đã chọn: {articleCount}</p>
                    <p>Số lượng người dùng trong khoảng thời gian đã chọn: {userCount}</p>
                    <p>Số lượng bình luận trong khoảng thời gian đã chọn: {commentCount}</p>
                </div>
            )}
        </Container>
    )
}

export default Stats;