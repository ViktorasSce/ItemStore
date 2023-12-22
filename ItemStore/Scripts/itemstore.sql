CREATE TABLE item (
	id serial PRIMARY KEY,
	name varchar(50),
	price decimal,
	created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);