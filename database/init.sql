CREATE TABLE sensor_data (
    id SERIAL PRIMARY KEY,
    sensorid INT NOT NULL,
    timestamp TIMESTAMP NOT NULL,
    Value FLOAT NOT NULL
);

INSERT INTO sensor_data
(sensorid, "timestamp", value)
VALUES(1, '2026-03-18 22:36:23.436', 10);

INSERT INTO sensor_data
(sensorid, "timestamp", value)
VALUES(2, '2026-03-18 22:36:23.436', 20);

INSERT INTO sensor_data
(sensorid, "timestamp", value)
VALUES(3, '2026-03-18 22:36:23.436', 30);