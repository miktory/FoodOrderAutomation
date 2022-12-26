--
-- PostgreSQL database dump
--

-- Dumped from database version 13.9 (Ubuntu 13.9-1.pgdg20.04+1)
-- Dumped by pg_dump version 13.9 (Ubuntu 13.9-1.pgdg20.04+1)

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: btree_gin; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS btree_gin WITH SCHEMA public;


--
-- Name: EXTENSION btree_gin; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION btree_gin IS 'support for indexing common datatypes in GIN';


--
-- Name: btree_gist; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS btree_gist WITH SCHEMA public;


--
-- Name: EXTENSION btree_gist; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION btree_gist IS 'support for indexing common datatypes in GiST';


--
-- Name: citext; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS citext WITH SCHEMA public;


--
-- Name: EXTENSION citext; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION citext IS 'data type for case-insensitive character strings';


--
-- Name: cube; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS cube WITH SCHEMA public;


--
-- Name: EXTENSION cube; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION cube IS 'data type for multidimensional cubes';


--
-- Name: dblink; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS dblink WITH SCHEMA public;


--
-- Name: EXTENSION dblink; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION dblink IS 'connect to other PostgreSQL databases from within a database';


--
-- Name: dict_int; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS dict_int WITH SCHEMA public;


--
-- Name: EXTENSION dict_int; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION dict_int IS 'text search dictionary template for integers';


--
-- Name: dict_xsyn; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS dict_xsyn WITH SCHEMA public;


--
-- Name: EXTENSION dict_xsyn; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION dict_xsyn IS 'text search dictionary template for extended synonym processing';


--
-- Name: earthdistance; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS earthdistance WITH SCHEMA public;


--
-- Name: EXTENSION earthdistance; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION earthdistance IS 'calculate great-circle distances on the surface of the Earth';


--
-- Name: fuzzystrmatch; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS fuzzystrmatch WITH SCHEMA public;


--
-- Name: EXTENSION fuzzystrmatch; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION fuzzystrmatch IS 'determine similarities and distance between strings';


--
-- Name: hstore; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS hstore WITH SCHEMA public;


--
-- Name: EXTENSION hstore; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION hstore IS 'data type for storing sets of (key, value) pairs';


--
-- Name: intarray; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS intarray WITH SCHEMA public;


--
-- Name: EXTENSION intarray; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION intarray IS 'functions, operators, and index support for 1-D arrays of integers';


--
-- Name: ltree; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS ltree WITH SCHEMA public;


--
-- Name: EXTENSION ltree; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION ltree IS 'data type for hierarchical tree-like structures';


--
-- Name: pg_stat_statements; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS pg_stat_statements WITH SCHEMA public;


--
-- Name: EXTENSION pg_stat_statements; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION pg_stat_statements IS 'track planning and execution statistics of all SQL statements executed';


--
-- Name: pg_trgm; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS pg_trgm WITH SCHEMA public;


--
-- Name: EXTENSION pg_trgm; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION pg_trgm IS 'text similarity measurement and index searching based on trigrams';


--
-- Name: pgcrypto; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS pgcrypto WITH SCHEMA public;


--
-- Name: EXTENSION pgcrypto; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION pgcrypto IS 'cryptographic functions';


--
-- Name: pgrowlocks; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS pgrowlocks WITH SCHEMA public;


--
-- Name: EXTENSION pgrowlocks; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION pgrowlocks IS 'show row-level locking information';


--
-- Name: pgstattuple; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS pgstattuple WITH SCHEMA public;


--
-- Name: EXTENSION pgstattuple; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION pgstattuple IS 'show tuple-level statistics';


--
-- Name: tablefunc; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS tablefunc WITH SCHEMA public;


--
-- Name: EXTENSION tablefunc; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION tablefunc IS 'functions that manipulate whole tables, including crosstab';


--
-- Name: unaccent; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS unaccent WITH SCHEMA public;


--
-- Name: EXTENSION unaccent; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION unaccent IS 'text search dictionary that removes accents';


--
-- Name: uuid-ossp; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS "uuid-ossp" WITH SCHEMA public;


--
-- Name: EXTENSION "uuid-ossp"; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION "uuid-ossp" IS 'generate universally unique identifiers (UUIDs)';


--
-- Name: xml2; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS xml2 WITH SCHEMA public;


--
-- Name: EXTENSION xml2; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION xml2 IS 'XPath querying and XSLT';


--
-- Name: User Info_ID_seq; Type: SEQUENCE; Schema: public; Owner: rlvreweh
--

CREATE SEQUENCE public."User Info_ID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."User Info_ID_seq" OWNER TO rlvreweh;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: balance; Type: TABLE; Schema: public; Owner: rlvreweh
--

CREATE TABLE public.balance (
    id bigint NOT NULL,
    balance bigint NOT NULL
);


ALTER TABLE public.balance OWNER TO rlvreweh;

--
-- Name: date_menu; Type: TABLE; Schema: public; Owner: rlvreweh
--

CREATE TABLE public.date_menu (
    dish_id bigint NOT NULL,
    category_id bigint NOT NULL,
    date date NOT NULL
);


ALTER TABLE public.date_menu OWNER TO rlvreweh;

--
-- Name: dish_categories; Type: TABLE; Schema: public; Owner: rlvreweh
--

CREATE TABLE public.dish_categories (
    id bigint NOT NULL,
    name character varying(30) NOT NULL
);


ALTER TABLE public.dish_categories OWNER TO rlvreweh;

--
-- Name: dish_categories_id_seq; Type: SEQUENCE; Schema: public; Owner: rlvreweh
--

CREATE SEQUENCE public.dish_categories_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.dish_categories_id_seq OWNER TO rlvreweh;

--
-- Name: dish_categories_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: rlvreweh
--

ALTER SEQUENCE public.dish_categories_id_seq OWNED BY public.dish_categories.id;


--
-- Name: dish_list; Type: TABLE; Schema: public; Owner: rlvreweh
--

CREATE TABLE public.dish_list (
    id bigint NOT NULL,
    category_id bigint NOT NULL,
    name character varying(20) NOT NULL,
    description character varying(100),
    cost double precision NOT NULL
);


ALTER TABLE public.dish_list OWNER TO rlvreweh;

--
-- Name: dish_list_id_seq; Type: SEQUENCE; Schema: public; Owner: rlvreweh
--

CREATE SEQUENCE public.dish_list_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.dish_list_id_seq OWNER TO rlvreweh;

--
-- Name: dish_list_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: rlvreweh
--

ALTER SEQUENCE public.dish_list_id_seq OWNED BY public.dish_list.id;


--
-- Name: grades; Type: TABLE; Schema: public; Owner: rlvreweh
--

CREATE TABLE public.grades (
    id bigint NOT NULL,
    name character varying(30) NOT NULL,
    teacher_id bigint NOT NULL
);


ALTER TABLE public.grades OWNER TO rlvreweh;

--
-- Name: grades_id_seq; Type: SEQUENCE; Schema: public; Owner: rlvreweh
--

CREATE SEQUENCE public.grades_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.grades_id_seq OWNER TO rlvreweh;

--
-- Name: grades_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: rlvreweh
--

ALTER SEQUENCE public.grades_id_seq OWNED BY public.grades.id;


--
-- Name: notifications; Type: TABLE; Schema: public; Owner: rlvreweh
--

CREATE TABLE public.notifications (
    notification character varying NOT NULL
);


ALTER TABLE public.notifications OWNER TO rlvreweh;

--
-- Name: order_details; Type: TABLE; Schema: public; Owner: rlvreweh
--

CREATE TABLE public.order_details (
    user_id bigint NOT NULL,
    dish_id bigint NOT NULL,
    order_date date NOT NULL,
    approved boolean DEFAULT false NOT NULL
);


ALTER TABLE public.order_details OWNER TO rlvreweh;

--
-- Name: orders; Type: TABLE; Schema: public; Owner: rlvreweh
--

CREATE TABLE public.orders (
    user_id bigint NOT NULL,
    status boolean DEFAULT false NOT NULL,
    order_date date NOT NULL,
    cost integer NOT NULL
);


ALTER TABLE public.orders OWNER TO rlvreweh;

--
-- Name: related_id; Type: TABLE; Schema: public; Owner: rlvreweh
--

CREATE TABLE public.related_id (
    id bigint NOT NULL,
    related_id bigint
);


ALTER TABLE public.related_id OWNER TO rlvreweh;

--
-- Name: roles_description; Type: TABLE; Schema: public; Owner: rlvreweh
--

CREATE TABLE public.roles_description (
    id bigint NOT NULL,
    name character varying(30) NOT NULL
);


ALTER TABLE public.roles_description OWNER TO rlvreweh;

--
-- Name: roles_description_id_seq; Type: SEQUENCE; Schema: public; Owner: rlvreweh
--

CREATE SEQUENCE public.roles_description_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.roles_description_id_seq OWNER TO rlvreweh;

--
-- Name: roles_description_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: rlvreweh
--

ALTER SEQUENCE public.roles_description_id_seq OWNED BY public.roles_description.id;


--
-- Name: students; Type: TABLE; Schema: public; Owner: rlvreweh
--

CREATE TABLE public.students (
    id bigint NOT NULL,
    grade_id bigint NOT NULL
);


ALTER TABLE public.students OWNER TO rlvreweh;

--
-- Name: user_info; Type: TABLE; Schema: public; Owner: rlvreweh
--

CREATE TABLE public.user_info (
    id bigint DEFAULT nextval('public."User Info_ID_seq"'::regclass) NOT NULL,
    login character varying(30) NOT NULL,
    password character varying(30) NOT NULL,
    name character varying(30) NOT NULL,
    surname character varying(30) NOT NULL,
    patronymic character varying(30) NOT NULL,
    selected_date date NOT NULL,
    selected_menu bigint DEFAULT 1 NOT NULL,
    selected_role bigint DEFAULT 2 NOT NULL
);


ALTER TABLE public.user_info OWNER TO rlvreweh;

--
-- Name: user_menus; Type: TABLE; Schema: public; Owner: rlvreweh
--

CREATE TABLE public.user_menus (
    user_id bigint NOT NULL,
    menu_id bigint NOT NULL,
    menu_name character varying NOT NULL
);


ALTER TABLE public.user_menus OWNER TO rlvreweh;

--
-- Name: user_menus_dishes; Type: TABLE; Schema: public; Owner: rlvreweh
--

CREATE TABLE public.user_menus_dishes (
    user_id bigint NOT NULL,
    menu_id bigint NOT NULL,
    dish_id bigint NOT NULL
);


ALTER TABLE public.user_menus_dishes OWNER TO rlvreweh;

--
-- Name: user_roles; Type: TABLE; Schema: public; Owner: rlvreweh
--

CREATE TABLE public.user_roles (
    user_id bigint NOT NULL,
    role_id bigint NOT NULL
);


ALTER TABLE public.user_roles OWNER TO rlvreweh;

--
-- Name: dish_categories id; Type: DEFAULT; Schema: public; Owner: rlvreweh
--

ALTER TABLE ONLY public.dish_categories ALTER COLUMN id SET DEFAULT nextval('public.dish_categories_id_seq'::regclass);


--
-- Name: dish_list id; Type: DEFAULT; Schema: public; Owner: rlvreweh
--

ALTER TABLE ONLY public.dish_list ALTER COLUMN id SET DEFAULT nextval('public.dish_list_id_seq'::regclass);


--
-- Name: grades id; Type: DEFAULT; Schema: public; Owner: rlvreweh
--

ALTER TABLE ONLY public.grades ALTER COLUMN id SET DEFAULT nextval('public.grades_id_seq'::regclass);


--
-- Name: roles_description id; Type: DEFAULT; Schema: public; Owner: rlvreweh
--

ALTER TABLE ONLY public.roles_description ALTER COLUMN id SET DEFAULT nextval('public.roles_description_id_seq'::regclass);


--
-- Data for Name: balance; Type: TABLE DATA; Schema: public; Owner: rlvreweh
--

COPY public.balance (id, balance) FROM stdin;
34	2000
33	2000
30	3000
32	1620
29	1320
31	1770
14	1682
15	1700
\.


--
-- Data for Name: date_menu; Type: TABLE DATA; Schema: public; Owner: rlvreweh
--

COPY public.date_menu (dish_id, category_id, date) FROM stdin;
11	1	2022-12-16
13	1	2022-12-16
15	2	2022-12-16
11	1	2022-12-17
13	1	2022-12-17
15	2	2022-12-17
16	2	2022-12-17
11	1	2022-12-18
13	1	2022-12-18
15	2	2022-12-18
16	2	2022-12-18
11	1	2022-12-20
13	1	2022-12-20
15	2	2022-12-20
16	2	2022-12-20
11	1	2022-12-21
13	1	2022-12-21
15	2	2022-12-21
16	2	2022-12-21
11	1	2022-12-22
13	1	2022-12-22
16	2	2022-12-22
15	2	2022-12-22
11	1	2022-12-23
13	1	2022-12-23
16	2	2022-12-23
15	2	2022-12-23
17	1	2022-12-23
11	1	2022-12-26
13	1	2022-12-26
17	1	2022-12-26
16	2	2022-12-26
15	2	2022-12-26
20	2	2022-12-26
18	3	2022-12-26
19	3	2022-12-26
\.


--
-- Data for Name: dish_categories; Type: TABLE DATA; Schema: public; Owner: rlvreweh
--

COPY public.dish_categories (id, name) FROM stdin;
2	Второе
1	Суп
3	Напиток
\.


--
-- Data for Name: dish_list; Type: TABLE DATA; Schema: public; Owner: rlvreweh
--

COPY public.dish_list (id, category_id, name, description, cost) FROM stdin;
16	2	Макароны с рыбой	Стандартная порция	200
15	2	Пюре с котлетой	Стандартная порция	150
11	1	Куриный суп	Описание	150
17	1	Харчо	обычная порция	200
13	1	Щи	Описание	150
18	3	Компот	200 мл	20
19	3	Чай	200 мл	30
20	2	Рис с гуляшом	Стандартная порция	130
\.


--
-- Data for Name: grades; Type: TABLE DATA; Schema: public; Owner: rlvreweh
--

COPY public.grades (id, name, teacher_id) FROM stdin;
2	9 <Б>	14
3	9 <В>	14
1	9 <А>	14
\.


--
-- Data for Name: notifications; Type: TABLE DATA; Schema: public; Owner: rlvreweh
--

COPY public.notifications (notification) FROM stdin;
Тестовое уведомление
\.


--
-- Data for Name: order_details; Type: TABLE DATA; Schema: public; Owner: rlvreweh
--

COPY public.order_details (user_id, dish_id, order_date, approved) FROM stdin;
14	13	2022-12-21	t
15	11	2022-12-21	t
15	11	2022-12-22	f
14	17	2022-12-23	t
15	11	2022-12-23	t
14	17	2022-12-26	t
15	11	2022-12-26	t
15	13	2022-12-26	t
31	17	2022-12-26	t
31	19	2022-12-26	t
32	11	2022-12-26	t
32	19	2022-12-26	t
32	16	2022-12-26	t
29	11	2022-12-26	t
29	15	2022-12-26	t
29	16	2022-12-26	t
29	13	2022-12-26	t
29	19	2022-12-26	t
\.


--
-- Data for Name: orders; Type: TABLE DATA; Schema: public; Owner: rlvreweh
--

COPY public.orders (user_id, status, order_date, cost) FROM stdin;
14	t	2022-12-21	1234
15	t	2022-12-21	1234
15	f	2022-12-22	1234
14	t	2022-12-23	500
15	t	2022-12-23	1234
14	t	2022-12-26	200
15	t	2022-12-26	300
31	t	2022-12-26	230
32	t	2022-12-26	380
29	t	2022-12-26	680
\.


--
-- Data for Name: related_id; Type: TABLE DATA; Schema: public; Owner: rlvreweh
--

COPY public.related_id (id, related_id) FROM stdin;
14	15
14	29
\.


--
-- Data for Name: roles_description; Type: TABLE DATA; Schema: public; Owner: rlvreweh
--

COPY public.roles_description (id, name) FROM stdin;
1	Кл. Рук.
2	Ученик
3	Родитель
4	Сотрудник
5	Админ.
\.


--
-- Data for Name: students; Type: TABLE DATA; Schema: public; Owner: rlvreweh
--

COPY public.students (id, grade_id) FROM stdin;
15	1
30	1
31	1
32	1
33	1
34	1
29	1
\.


--
-- Data for Name: user_info; Type: TABLE DATA; Schema: public; Owner: rlvreweh
--

COPY public.user_info (id, login, password, name, surname, patronymic, selected_date, selected_menu, selected_role) FROM stdin;
30	alekseev_george	1234567890	Георгий	Алексеев	Александрович	2022-12-26	1	2
34	popov_konstantin	1234567890	Константин	Попов	Константинович	2022-12-26	1	2
32	bondareva_kristina	1234567890	Кристина	Бондарева	Романовна	2022-12-26	1	2
29	grigoriev_yuri	1234567890	Юрий	Григорьев	Александрович	2022-12-26	1	2
31	zavadskaia_anastasia	1234567890	Анастасия	Завадская	Сергеевна	2022-12-26	1	2
33	petrova_anna	1234567890	Анна	Петрова	Владимировна	2022-12-26	1	2
15	test_user	1234567890	Константин	Иванов	Андреевич	2022-12-26	1	2
14	anna_guseva	1234567890	Анна	Гусева	Константиновна	2022-12-26	1	5
\.


--
-- Data for Name: user_menus; Type: TABLE DATA; Schema: public; Owner: rlvreweh
--

COPY public.user_menus (user_id, menu_id, menu_name) FROM stdin;
15	1	Моё меню
14	2	Menu 2
29	1	Основное меню
14	1	меню 3
30	1	Основное меню
31	1	Основное меню
32	1	Основное меню
33	1	Основное меню
34	1	Основное меню
\.


--
-- Data for Name: user_menus_dishes; Type: TABLE DATA; Schema: public; Owner: rlvreweh
--

COPY public.user_menus_dishes (user_id, menu_id, dish_id) FROM stdin;
14	2	13
15	1	11
29	1	11
29	1	15
29	1	16
29	1	13
14	1	17
32	1	11
32	1	19
32	1	16
29	1	19
31	1	17
31	1	19
33	1	15
33	1	18
15	1	13
\.


--
-- Data for Name: user_roles; Type: TABLE DATA; Schema: public; Owner: rlvreweh
--

COPY public.user_roles (user_id, role_id) FROM stdin;
14	1
14	3
15	2
14	4
14	5
29	2
30	2
31	2
32	2
33	2
34	2
\.


--
-- Name: User Info_ID_seq; Type: SEQUENCE SET; Schema: public; Owner: rlvreweh
--

SELECT pg_catalog.setval('public."User Info_ID_seq"', 34, true);


--
-- Name: dish_categories_id_seq; Type: SEQUENCE SET; Schema: public; Owner: rlvreweh
--

SELECT pg_catalog.setval('public.dish_categories_id_seq', 3, true);


--
-- Name: dish_list_id_seq; Type: SEQUENCE SET; Schema: public; Owner: rlvreweh
--

SELECT pg_catalog.setval('public.dish_list_id_seq', 20, true);


--
-- Name: grades_id_seq; Type: SEQUENCE SET; Schema: public; Owner: rlvreweh
--

SELECT pg_catalog.setval('public.grades_id_seq', 16, true);


--
-- Name: roles_description_id_seq; Type: SEQUENCE SET; Schema: public; Owner: rlvreweh
--

SELECT pg_catalog.setval('public.roles_description_id_seq', 5, true);


--
-- Name: user_info User Info_pkey; Type: CONSTRAINT; Schema: public; Owner: rlvreweh
--

ALTER TABLE ONLY public.user_info
    ADD CONSTRAINT "User Info_pkey" PRIMARY KEY (id);


--
-- Name: dish_categories dish_categories_pkey; Type: CONSTRAINT; Schema: public; Owner: rlvreweh
--

ALTER TABLE ONLY public.dish_categories
    ADD CONSTRAINT dish_categories_pkey PRIMARY KEY (id);


--
-- Name: dish_list dish_list_pkey; Type: CONSTRAINT; Schema: public; Owner: rlvreweh
--

ALTER TABLE ONLY public.dish_list
    ADD CONSTRAINT dish_list_pkey PRIMARY KEY (id);


--
-- Name: grades grades_pkey; Type: CONSTRAINT; Schema: public; Owner: rlvreweh
--

ALTER TABLE ONLY public.grades
    ADD CONSTRAINT grades_pkey PRIMARY KEY (id);


--
-- Name: user_info login; Type: CONSTRAINT; Schema: public; Owner: rlvreweh
--

ALTER TABLE ONLY public.user_info
    ADD CONSTRAINT login UNIQUE (login);


--
-- Name: roles_description roles_description_pkey; Type: CONSTRAINT; Schema: public; Owner: rlvreweh
--

ALTER TABLE ONLY public.roles_description
    ADD CONSTRAINT roles_description_pkey PRIMARY KEY (id);


--
-- Name: students students_pkey; Type: CONSTRAINT; Schema: public; Owner: rlvreweh
--

ALTER TABLE ONLY public.students
    ADD CONSTRAINT students_pkey PRIMARY KEY (id);


--
-- Name: user_roles user_id and role_id; Type: CONSTRAINT; Schema: public; Owner: rlvreweh
--

ALTER TABLE ONLY public.user_roles
    ADD CONSTRAINT "user_id and role_id" UNIQUE (user_id, role_id);


--
-- Name: dish_list category_id; Type: FK CONSTRAINT; Schema: public; Owner: rlvreweh
--

ALTER TABLE ONLY public.dish_list
    ADD CONSTRAINT category_id FOREIGN KEY (category_id) REFERENCES public.dish_categories(id) ON UPDATE CASCADE ON DELETE RESTRICT NOT VALID;


--
-- Name: date_menu category_id; Type: FK CONSTRAINT; Schema: public; Owner: rlvreweh
--

ALTER TABLE ONLY public.date_menu
    ADD CONSTRAINT category_id FOREIGN KEY (category_id) REFERENCES public.dish_categories(id) ON UPDATE CASCADE ON DELETE RESTRICT NOT VALID;


--
-- Name: date_menu dish_id; Type: FK CONSTRAINT; Schema: public; Owner: rlvreweh
--

ALTER TABLE ONLY public.date_menu
    ADD CONSTRAINT dish_id FOREIGN KEY (dish_id) REFERENCES public.dish_list(id) ON UPDATE CASCADE ON DELETE RESTRICT;


--
-- Name: user_menus_dishes dish_id; Type: FK CONSTRAINT; Schema: public; Owner: rlvreweh
--

ALTER TABLE ONLY public.user_menus_dishes
    ADD CONSTRAINT dish_id FOREIGN KEY (dish_id) REFERENCES public.dish_list(id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- Name: order_details dish_id; Type: FK CONSTRAINT; Schema: public; Owner: rlvreweh
--

ALTER TABLE ONLY public.order_details
    ADD CONSTRAINT dish_id FOREIGN KEY (dish_id) REFERENCES public.dish_list(id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- Name: students grade_id; Type: FK CONSTRAINT; Schema: public; Owner: rlvreweh
--

ALTER TABLE ONLY public.students
    ADD CONSTRAINT grade_id FOREIGN KEY (grade_id) REFERENCES public.grades(id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- Name: related_id related_id; Type: FK CONSTRAINT; Schema: public; Owner: rlvreweh
--

ALTER TABLE ONLY public.related_id
    ADD CONSTRAINT related_id FOREIGN KEY (related_id) REFERENCES public.user_info(id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- Name: user_roles role_id; Type: FK CONSTRAINT; Schema: public; Owner: rlvreweh
--

ALTER TABLE ONLY public.user_roles
    ADD CONSTRAINT role_id FOREIGN KEY (role_id) REFERENCES public.roles_description(id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- Name: students student_id; Type: FK CONSTRAINT; Schema: public; Owner: rlvreweh
--

ALTER TABLE ONLY public.students
    ADD CONSTRAINT student_id FOREIGN KEY (id) REFERENCES public.user_info(id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- Name: grades teacher_id; Type: FK CONSTRAINT; Schema: public; Owner: rlvreweh
--

ALTER TABLE ONLY public.grades
    ADD CONSTRAINT teacher_id FOREIGN KEY (teacher_id) REFERENCES public.user_info(id) ON UPDATE CASCADE ON DELETE RESTRICT NOT VALID;


--
-- Name: user_roles user_id; Type: FK CONSTRAINT; Schema: public; Owner: rlvreweh
--

ALTER TABLE ONLY public.user_roles
    ADD CONSTRAINT user_id FOREIGN KEY (user_id) REFERENCES public.user_info(id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- Name: user_menus_dishes user_id; Type: FK CONSTRAINT; Schema: public; Owner: rlvreweh
--

ALTER TABLE ONLY public.user_menus_dishes
    ADD CONSTRAINT user_id FOREIGN KEY (user_id) REFERENCES public.user_info(id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- Name: user_menus user_id; Type: FK CONSTRAINT; Schema: public; Owner: rlvreweh
--

ALTER TABLE ONLY public.user_menus
    ADD CONSTRAINT user_id FOREIGN KEY (user_id) REFERENCES public.user_info(id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- Name: related_id user_id; Type: FK CONSTRAINT; Schema: public; Owner: rlvreweh
--

ALTER TABLE ONLY public.related_id
    ADD CONSTRAINT user_id FOREIGN KEY (id) REFERENCES public.user_info(id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- Name: orders user_id; Type: FK CONSTRAINT; Schema: public; Owner: rlvreweh
--

ALTER TABLE ONLY public.orders
    ADD CONSTRAINT user_id FOREIGN KEY (user_id) REFERENCES public.user_info(id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- Name: order_details user_id; Type: FK CONSTRAINT; Schema: public; Owner: rlvreweh
--

ALTER TABLE ONLY public.order_details
    ADD CONSTRAINT user_id FOREIGN KEY (user_id) REFERENCES public.user_info(id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- Name: balance user_id; Type: FK CONSTRAINT; Schema: public; Owner: rlvreweh
--

ALTER TABLE ONLY public.balance
    ADD CONSTRAINT user_id FOREIGN KEY (id) REFERENCES public.user_info(id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- PostgreSQL database dump complete
--

