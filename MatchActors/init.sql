CREATE TABLE "actors"
(
    id       uuid primary key,
    actor_id text unique not null,
    name     text not null
);