import axios from "axios";
import { useState } from "react"
import { Button, Container, Form, Table } from "react-bootstrap";

const Country = () => {
    const [country, setCountry] = useState();
    const [countryInfo, setCountryInfo] = useState();

    const handleSubmit = async (e) => {
        e.preventDefault();

        try {
            const res = await axios.get(`https://localhost:7019/api/Country/${country}`)
            if (res.status === 200) {
                setCountryInfo(res.data);
            } else {
                alert("Tên quốc gia sai hoặc có lỗi xảy ra");
                setCountryInfo(null);
            }
            console.log(res);
        } catch (error) {
            console.error("failed", error);
        }
    }

    return (
        <Container>
            <h1 className="text-center text-success">Thông tin quốc gia</h1>
            <Form onSubmit={handleSubmit} className="mb-3 mt-2">
                <Form.Group>
                    <Form.Control
                        type="text"
                        placeholder="Nhập tên quốc gia bằng tiếng Anh"
                        value={country}
                        onChange={(e) => { setCountry(e.target.value); }}
                        required
                    />
                </Form.Group>
                <Button variant="primary" type="submit" className='mt-2'>
                    Tìm kiếm
                </Button>
            </Form>
            {countryInfo && (
                <Table className="mt-3" striped bordered hover>
                    <thead>
                        <tr>
                            <th>Tên thông thường</th>
                            <th>Tên chính thống</th>
                            <th>Thủ đô</th>
                            <th>Dân số</th>
                            <th>Khu vực</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>{countryInfo[0].name.common}</td>
                            <td>{countryInfo[0].name.official}</td>
                            <td>{countryInfo[0].capital[0]}</td>
                            <td>{countryInfo[0].population}</td>
                            <td>{countryInfo[0].region}</td>
                        </tr>
                    </tbody>
                </Table>
            )}
        </Container>
    )
}

export default Country;