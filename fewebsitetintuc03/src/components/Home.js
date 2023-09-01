import React, { useEffect, useState } from 'react';
import { Card, Container, Row, Col } from 'react-bootstrap';
import Axios from 'axios';
import { Link } from 'react-router-dom';
import MySpinner from '../layout/MySpinner';

const Home = () => {
  const [articles, setArticles] = useState([]);

  const loadArticles = async () => {
    try {
      const response = await Axios.post('https://localhost:7019/api/Article/search-article', {
        keyword: '',
        page: 1,
        size: 10,
      });
      setArticles(response.data.data.data);
      console.log(response.data.data.data);
    } catch (error) {
      console.error('failed', error);
    }
  };

  useEffect(() => {
    loadArticles();
  }, [])

  return (
    <Container>
      <Row className="mt-2 mb-2">
        {articles && articles.map((article) => (
          <Col key={article.id} md={3} className="mt-2 mb-2">
            <Card style={{ width: '18rem' }}>
              <Card.Img variant="top" src="/logo512.png" />
              <Card.Body>
                <Card.Title>{article.title}</Card.Title>
                <Link variant="primary" className='btn btn-success' to={`articles/${article.id}`}>Xem chi tiết</Link>
              </Card.Body>
            </Card>
          </Col>
        ))}
      </Row>
    </Container>
  );
}

export default Home;