import React, { useState } from 'react';
import { Card, Button, Container, Row, Col } from 'react-bootstrap';

const Home = () => {
    const [articles, setArticles] = useState([]);
    const articless = [
        {
          id: 1,
          title: 'Tiêu đề bài báo 1',
          imageUrl: 'URL_ANH_ARTICLE_1',
          summary: 'Nội dung tóm tắt bài báo 1',
        },
        {
          id: 2,
          title: 'Tiêu đề bài báo 2',
          imageUrl: 'URL_ANH_ARTICLE_2',
          summary: 'Nội dung tóm tắt bài báo 2',
        },
        {
          id: 3,
          title: 'Tiêu đề bài báo 3',
          imageUrl: 'URL_ANH_ARTICLE_3',
          summary: 'Nội dung tóm tắt bài báo 3',
        },
        // Thêm các bài báo khác tương tự ở đây nếu cần
      ];
    
      return (
        <Container>
      <Row className="mt-2 mb-2">
        {articless.map((article) => (
          <Col key={article.id} md={4}>
            <Card style={{ width: '18rem' }}>
              <Card.Img variant="top" src="/logo512.png" />
              <Card.Body>
                <Card.Title>{article.title}</Card.Title>
                <Button variant="primary">Xem chi tiết</Button>
              </Card.Body>
            </Card>
          </Col>
        ))}
      </Row>
    </Container>
      );
    }

export default Home;