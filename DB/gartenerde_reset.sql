PGDMP                         y           mct    10.11    10.11 �    �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                       false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                       false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                       false            �           1262    220586    mct    DATABASE     �   CREATE DATABASE mct WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'German_Germany.1252' LC_CTYPE = 'German_Germany.1252';
    DROP DATABASE mct;
             postgres    false                        2615    2200    public    SCHEMA        CREATE SCHEMA public;
    DROP SCHEMA public;
             postgres    false            �           0    0    SCHEMA public    COMMENT     6   COMMENT ON SCHEMA public IS 'standard public schema';
                  postgres    false    3                        3079    12924    plpgsql 	   EXTENSION     ?   CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;
    DROP EXTENSION plpgsql;
                  false            �           0    0    EXTENSION plpgsql    COMMENT     @   COMMENT ON EXTENSION plpgsql IS 'PL/pgSQL procedural language';
                       false    1            �            1259    224146    aftercultures_mappping    TABLE     m   CREATE TABLE public.aftercultures_mappping (
    id bigint NOT NULL,
    aftercultures_id bigint NOT NULL
);
 *   DROP TABLE public.aftercultures_mappping;
       public         postgres    false    3            �            1259    224151    animals    TABLE     8   CREATE TABLE public.animals (
    id bigint NOT NULL
);
    DROP TABLE public.animals;
       public         postgres    false    3            �            1259    224224    blooms    TABLE     7   CREATE TABLE public.blooms (
    id bigint NOT NULL
);
    DROP TABLE public.blooms;
       public         postgres    false    3            �            1259    224229 
   cultivates    TABLE     �   CREATE TABLE public.cultivates (
    id bigint NOT NULL,
    germinationperioddays integer,
    germinationtemperature double precision
);
    DROP TABLE public.cultivates;
       public         postgres    false    3            �            1259    224185    days    TABLE     e   CREATE TABLE public.days (
    id bigint NOT NULL,
    dayinyear integer,
    weekperyear integer
);
    DROP TABLE public.days;
       public         postgres    false    3            �            1259    224183    days_id_seq    SEQUENCE     t   CREATE SEQUENCE public.days_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 "   DROP SEQUENCE public.days_id_seq;
       public       postgres    false    3    213            �           0    0    days_id_seq    SEQUENCE OWNED BY     ;   ALTER SEQUENCE public.days_id_seq OWNED BY public.days.id;
            public       postgres    false    212            �            1259    224126    effects    TABLE     8   CREATE TABLE public.effects (
    id bigint NOT NULL
);
    DROP TABLE public.effects;
       public         postgres    false    3            �            1259    224214    harvests    TABLE     9   CREATE TABLE public.harvests (
    id bigint NOT NULL
);
    DROP TABLE public.harvests;
       public         postgres    false    3            �            1259    224234    implants    TABLE     9   CREATE TABLE public.implants (
    id bigint NOT NULL
);
    DROP TABLE public.implants;
       public         postgres    false    3            �            1259    224193    interactions    TABLE     �   CREATE TABLE public.interactions (
    id bigint NOT NULL,
    subject bigint NOT NULL,
    predicate bigint NOT NULL,
    object bigint NOT NULL,
    impactsubject bigint,
    indicator integer
);
     DROP TABLE public.interactions;
       public         postgres    false    3            �            1259    224191    interactions_id_seq    SEQUENCE     |   CREATE SEQUENCE public.interactions_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 *   DROP SEQUENCE public.interactions_id_seq;
       public       postgres    false    215    3            �           0    0    interactions_id_seq    SEQUENCE OWNED BY     K   ALTER SEQUENCE public.interactions_id_seq OWNED BY public.interactions.id;
            public       postgres    false    214            �            1259    224239 	   lifetimes    TABLE     :   CREATE TABLE public.lifetimes (
    id bigint NOT NULL
);
    DROP TABLE public.lifetimes;
       public         postgres    false    3            �            1259    224112    medias    TABLE     �   CREATE TABLE public.medias (
    id bigint NOT NULL,
    imagepath character varying(255),
    mimetype character varying(255),
    subject bigint
);
    DROP TABLE public.medias;
       public         postgres    false    3            �            1259    224110    medias_id_seq    SEQUENCE     v   CREATE SEQUENCE public.medias_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 $   DROP SEQUENCE public.medias_id_seq;
       public       postgres    false    199    3            �           0    0    medias_id_seq    SEQUENCE OWNED BY     ?   ALTER SEQUENCE public.medias_id_seq OWNED BY public.medias.id;
            public       postgres    false    198            �            1259    224174    names    TABLE     �   CREATE TABLE public.names (
    id bigint NOT NULL,
    node bigint NOT NULL,
    name character varying(255),
    language character varying(255),
    ispreferredname boolean
);
    DROP TABLE public.names;
       public         postgres    false    3            �            1259    224172    names_id_seq    SEQUENCE     u   CREATE SEQUENCE public.names_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 #   DROP SEQUENCE public.names_id_seq;
       public       postgres    false    211    3            �           0    0    names_id_seq    SEQUENCE OWNED BY     =   ALTER SEQUENCE public.names_id_seq OWNED BY public.names.id;
            public       postgres    false    210            �            1259    224121    nodes    TABLE     �   CREATE TABLE public.nodes (
    id bigint NOT NULL,
    scientificname character varying(255),
    rank integer,
    parent bigint
);
    DROP TABLE public.nodes;
       public         postgres    false    3            �            1259    224257    patchelements    TABLE     �   CREATE TABLE public.patchelements (
    id bigint NOT NULL,
    transformation character varying(255),
    patchref bigint NOT NULL
);
 !   DROP TABLE public.patchelements;
       public         postgres    false    3            �            1259    224255    patchelements_id_seq    SEQUENCE     }   CREATE SEQUENCE public.patchelements_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 +   DROP SEQUENCE public.patchelements_id_seq;
       public       postgres    false    3    228            �           0    0    patchelements_id_seq    SEQUENCE OWNED BY     M   ALTER SEQUENCE public.patchelements_id_seq OWNED BY public.patchelements.id;
            public       postgres    false    227            �            1259    224246    patches    TABLE     �   CREATE TABLE public.patches (
    id bigint NOT NULL,
    name character varying(255),
    description character varying(3000),
    width integer,
    height integer,
    locationtype integer,
    nutrientclaim integer
);
    DROP TABLE public.patches;
       public         postgres    false    3            �            1259    224244    patches_id_seq    SEQUENCE     w   CREATE SEQUENCE public.patches_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 %   DROP SEQUENCE public.patches_id_seq;
       public       postgres    false    3    226            �           0    0    patches_id_seq    SEQUENCE OWNED BY     A   ALTER SEQUENCE public.patches_id_seq OWNED BY public.patches.id;
            public       postgres    false    225            �            1259    224263 
   placements    TABLE     �   CREATE TABLE public.placements (
    id bigint NOT NULL,
    plantingarea integer,
    plantingmonth integer,
    plantref bigint
);
    DROP TABLE public.placements;
       public         postgres    false    3            �            1259    224136    plants    TABLE     �   CREATE TABLE public.plants (
    id bigint NOT NULL,
    width double precision,
    height double precision,
    rootdepth integer,
    nutrientclaim integer,
    locationtype integer,
    sowingdepth integer
);
    DROP TABLE public.plants;
       public         postgres    false    3            �            1259    224141    precultures_mappping    TABLE     i   CREATE TABLE public.precultures_mappping (
    id bigint NOT NULL,
    precultures_id bigint NOT NULL
);
 (   DROP TABLE public.precultures_mappping;
       public         postgres    false    3            �            1259    224163 
   predicates    TABLE     �   CREATE TABLE public.predicates (
    id bigint NOT NULL,
    name character varying(255),
    description character varying(255),
    parentref bigint
);
    DROP TABLE public.predicates;
       public         postgres    false    3            �            1259    224161    predicates_id_seq    SEQUENCE     z   CREATE SEQUENCE public.predicates_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 (   DROP SEQUENCE public.predicates_id_seq;
       public       postgres    false    209    3            �           0    0    predicates_id_seq    SEQUENCE OWNED BY     G   ALTER SEQUENCE public.predicates_id_seq OWNED BY public.predicates.id;
            public       postgres    false    208            �            1259    224219    seedmaturitys    TABLE     >   CREATE TABLE public.seedmaturitys (
    id bigint NOT NULL
);
 !   DROP TABLE public.seedmaturitys;
       public         postgres    false    3            �            1259    224209    sowings    TABLE     8   CREATE TABLE public.sowings (
    id bigint NOT NULL
);
    DROP TABLE public.sowings;
       public         postgres    false    3            �            1259    224131    species    TABLE     8   CREATE TABLE public.species (
    id bigint NOT NULL
);
    DROP TABLE public.species;
       public         postgres    false    3            �            1259    224101    subjects    TABLE     �   CREATE TABLE public.subjects (
    id bigint NOT NULL,
    name character varying(255),
    description character varying(3000)
);
    DROP TABLE public.subjects;
       public         postgres    false    3            �            1259    224099    subjects_id_seq    SEQUENCE     x   CREATE SEQUENCE public.subjects_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 &   DROP SEQUENCE public.subjects_id_seq;
       public       postgres    false    3    197            �           0    0    subjects_id_seq    SEQUENCE OWNED BY     C   ALTER SEQUENCE public.subjects_id_seq OWNED BY public.subjects.id;
            public       postgres    false    196            �            1259    224156    taxa    TABLE     5   CREATE TABLE public.taxa (
    id bigint NOT NULL
);
    DROP TABLE public.taxa;
       public         postgres    false    3            �            1259    224201    timeperiods    TABLE     �   CREATE TABLE public.timeperiods (
    id bigint NOT NULL,
    startarea integer,
    startmonth integer,
    endarea integer,
    endmonth integer,
    start boolean,
    nextid bigint,
    subjectref bigint
);
    DROP TABLE public.timeperiods;
       public         postgres    false    3            �            1259    224199    timeperiods_id_seq    SEQUENCE     {   CREATE SEQUENCE public.timeperiods_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 )   DROP SEQUENCE public.timeperiods_id_seq;
       public       postgres    false    217    3            �           0    0    timeperiods_id_seq    SEQUENCE OWNED BY     I   ALTER SEQUENCE public.timeperiods_id_seq OWNED BY public.timeperiods.id;
            public       postgres    false    216            �
           2604    224188    days id    DEFAULT     b   ALTER TABLE ONLY public.days ALTER COLUMN id SET DEFAULT nextval('public.days_id_seq'::regclass);
 6   ALTER TABLE public.days ALTER COLUMN id DROP DEFAULT;
       public       postgres    false    212    213    213            �
           2604    224196    interactions id    DEFAULT     r   ALTER TABLE ONLY public.interactions ALTER COLUMN id SET DEFAULT nextval('public.interactions_id_seq'::regclass);
 >   ALTER TABLE public.interactions ALTER COLUMN id DROP DEFAULT;
       public       postgres    false    214    215    215            �
           2604    224115 	   medias id    DEFAULT     f   ALTER TABLE ONLY public.medias ALTER COLUMN id SET DEFAULT nextval('public.medias_id_seq'::regclass);
 8   ALTER TABLE public.medias ALTER COLUMN id DROP DEFAULT;
       public       postgres    false    198    199    199            �
           2604    224177    names id    DEFAULT     d   ALTER TABLE ONLY public.names ALTER COLUMN id SET DEFAULT nextval('public.names_id_seq'::regclass);
 7   ALTER TABLE public.names ALTER COLUMN id DROP DEFAULT;
       public       postgres    false    210    211    211            �
           2604    224260    patchelements id    DEFAULT     t   ALTER TABLE ONLY public.patchelements ALTER COLUMN id SET DEFAULT nextval('public.patchelements_id_seq'::regclass);
 ?   ALTER TABLE public.patchelements ALTER COLUMN id DROP DEFAULT;
       public       postgres    false    228    227    228            �
           2604    224249 
   patches id    DEFAULT     h   ALTER TABLE ONLY public.patches ALTER COLUMN id SET DEFAULT nextval('public.patches_id_seq'::regclass);
 9   ALTER TABLE public.patches ALTER COLUMN id DROP DEFAULT;
       public       postgres    false    226    225    226            �
           2604    224166    predicates id    DEFAULT     n   ALTER TABLE ONLY public.predicates ALTER COLUMN id SET DEFAULT nextval('public.predicates_id_seq'::regclass);
 <   ALTER TABLE public.predicates ALTER COLUMN id DROP DEFAULT;
       public       postgres    false    208    209    209            �
           2604    224104    subjects id    DEFAULT     j   ALTER TABLE ONLY public.subjects ALTER COLUMN id SET DEFAULT nextval('public.subjects_id_seq'::regclass);
 :   ALTER TABLE public.subjects ALTER COLUMN id DROP DEFAULT;
       public       postgres    false    196    197    197            �
           2604    224204    timeperiods id    DEFAULT     p   ALTER TABLE ONLY public.timeperiods ALTER COLUMN id SET DEFAULT nextval('public.timeperiods_id_seq'::regclass);
 =   ALTER TABLE public.timeperiods ALTER COLUMN id DROP DEFAULT;
       public       postgres    false    217    216    217            �          0    224146    aftercultures_mappping 
   TABLE DATA               F   COPY public.aftercultures_mappping (id, aftercultures_id) FROM stdin;
    public       postgres    false    205   ��       �          0    224151    animals 
   TABLE DATA               %   COPY public.animals (id) FROM stdin;
    public       postgres    false    206   ��       �          0    224224    blooms 
   TABLE DATA               $   COPY public.blooms (id) FROM stdin;
    public       postgres    false    221   ��       �          0    224229 
   cultivates 
   TABLE DATA               W   COPY public.cultivates (id, germinationperioddays, germinationtemperature) FROM stdin;
    public       postgres    false    222   ע       �          0    224185    days 
   TABLE DATA               :   COPY public.days (id, dayinyear, weekperyear) FROM stdin;
    public       postgres    false    213   ��       �          0    224126    effects 
   TABLE DATA               %   COPY public.effects (id) FROM stdin;
    public       postgres    false    201   S�       �          0    224214    harvests 
   TABLE DATA               &   COPY public.harvests (id) FROM stdin;
    public       postgres    false    219   p�       �          0    224234    implants 
   TABLE DATA               &   COPY public.implants (id) FROM stdin;
    public       postgres    false    223   ��       �          0    224193    interactions 
   TABLE DATA               `   COPY public.interactions (id, subject, predicate, object, impactsubject, indicator) FROM stdin;
    public       postgres    false    215   ��       �          0    224239 	   lifetimes 
   TABLE DATA               '   COPY public.lifetimes (id) FROM stdin;
    public       postgres    false    224   Ǩ       �          0    224112    medias 
   TABLE DATA               B   COPY public.medias (id, imagepath, mimetype, subject) FROM stdin;
    public       postgres    false    199   �       �          0    224174    names 
   TABLE DATA               J   COPY public.names (id, node, name, language, ispreferredname) FROM stdin;
    public       postgres    false    211   �       �          0    224121    nodes 
   TABLE DATA               A   COPY public.nodes (id, scientificname, rank, parent) FROM stdin;
    public       postgres    false    200   �       �          0    224257    patchelements 
   TABLE DATA               E   COPY public.patchelements (id, transformation, patchref) FROM stdin;
    public       postgres    false    228   ;�       �          0    224246    patches 
   TABLE DATA               d   COPY public.patches (id, name, description, width, height, locationtype, nutrientclaim) FROM stdin;
    public       postgres    false    226   X�       �          0    224263 
   placements 
   TABLE DATA               O   COPY public.placements (id, plantingarea, plantingmonth, plantref) FROM stdin;
    public       postgres    false    229   u�       �          0    224136    plants 
   TABLE DATA               h   COPY public.plants (id, width, height, rootdepth, nutrientclaim, locationtype, sowingdepth) FROM stdin;
    public       postgres    false    203   ��       �          0    224141    precultures_mappping 
   TABLE DATA               B   COPY public.precultures_mappping (id, precultures_id) FROM stdin;
    public       postgres    false    204   ��       �          0    224163 
   predicates 
   TABLE DATA               F   COPY public.predicates (id, name, description, parentref) FROM stdin;
    public       postgres    false    209   ̩       �          0    224219    seedmaturitys 
   TABLE DATA               +   COPY public.seedmaturitys (id) FROM stdin;
    public       postgres    false    220   \�       �          0    224209    sowings 
   TABLE DATA               %   COPY public.sowings (id) FROM stdin;
    public       postgres    false    218   y�       �          0    224131    species 
   TABLE DATA               %   COPY public.species (id) FROM stdin;
    public       postgres    false    202   ��       �          0    224101    subjects 
   TABLE DATA               9   COPY public.subjects (id, name, description) FROM stdin;
    public       postgres    false    197   ��       �          0    224156    taxa 
   TABLE DATA               "   COPY public.taxa (id) FROM stdin;
    public       postgres    false    207   Ъ       �          0    224201    timeperiods 
   TABLE DATA               n   COPY public.timeperiods (id, startarea, startmonth, endarea, endmonth, start, nextid, subjectref) FROM stdin;
    public       postgres    false    217   ��       �           0    0    days_id_seq    SEQUENCE SET     ;   SELECT pg_catalog.setval('public.days_id_seq', 364, true);
            public       postgres    false    212            �           0    0    interactions_id_seq    SEQUENCE SET     B   SELECT pg_catalog.setval('public.interactions_id_seq', 1, false);
            public       postgres    false    214            �           0    0    medias_id_seq    SEQUENCE SET     <   SELECT pg_catalog.setval('public.medias_id_seq', 1, false);
            public       postgres    false    198            �           0    0    names_id_seq    SEQUENCE SET     ;   SELECT pg_catalog.setval('public.names_id_seq', 1, false);
            public       postgres    false    210            �           0    0    patchelements_id_seq    SEQUENCE SET     C   SELECT pg_catalog.setval('public.patchelements_id_seq', 1, false);
            public       postgres    false    227            �           0    0    patches_id_seq    SEQUENCE SET     =   SELECT pg_catalog.setval('public.patches_id_seq', 1, false);
            public       postgres    false    225            �           0    0    predicates_id_seq    SEQUENCE SET     ?   SELECT pg_catalog.setval('public.predicates_id_seq', 9, true);
            public       postgres    false    208            �           0    0    subjects_id_seq    SEQUENCE SET     >   SELECT pg_catalog.setval('public.subjects_id_seq', 1, false);
            public       postgres    false    196            �           0    0    timeperiods_id_seq    SEQUENCE SET     A   SELECT pg_catalog.setval('public.timeperiods_id_seq', 1, false);
            public       postgres    false    216            �
           2606    224150 2   aftercultures_mappping aftercultures_mappping_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public.aftercultures_mappping
    ADD CONSTRAINT aftercultures_mappping_pkey PRIMARY KEY (id, aftercultures_id);
 \   ALTER TABLE ONLY public.aftercultures_mappping DROP CONSTRAINT aftercultures_mappping_pkey;
       public         postgres    false    205    205            �
           2606    224155    animals animals_pkey 
   CONSTRAINT     R   ALTER TABLE ONLY public.animals
    ADD CONSTRAINT animals_pkey PRIMARY KEY (id);
 >   ALTER TABLE ONLY public.animals DROP CONSTRAINT animals_pkey;
       public         postgres    false    206                       2606    224228    blooms blooms_pkey 
   CONSTRAINT     P   ALTER TABLE ONLY public.blooms
    ADD CONSTRAINT blooms_pkey PRIMARY KEY (id);
 <   ALTER TABLE ONLY public.blooms DROP CONSTRAINT blooms_pkey;
       public         postgres    false    221                       2606    224233    cultivates cultivates_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY public.cultivates
    ADD CONSTRAINT cultivates_pkey PRIMARY KEY (id);
 D   ALTER TABLE ONLY public.cultivates DROP CONSTRAINT cultivates_pkey;
       public         postgres    false    222                       2606    224190    days days_pkey 
   CONSTRAINT     L   ALTER TABLE ONLY public.days
    ADD CONSTRAINT days_pkey PRIMARY KEY (id);
 8   ALTER TABLE ONLY public.days DROP CONSTRAINT days_pkey;
       public         postgres    false    213            �
           2606    224130    effects effects_pkey 
   CONSTRAINT     R   ALTER TABLE ONLY public.effects
    ADD CONSTRAINT effects_pkey PRIMARY KEY (id);
 >   ALTER TABLE ONLY public.effects DROP CONSTRAINT effects_pkey;
       public         postgres    false    201                       2606    224218    harvests harvests_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY public.harvests
    ADD CONSTRAINT harvests_pkey PRIMARY KEY (id);
 @   ALTER TABLE ONLY public.harvests DROP CONSTRAINT harvests_pkey;
       public         postgres    false    219                       2606    224238    implants implants_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY public.implants
    ADD CONSTRAINT implants_pkey PRIMARY KEY (id);
 @   ALTER TABLE ONLY public.implants DROP CONSTRAINT implants_pkey;
       public         postgres    false    223                       2606    224198    interactions interactions_pkey 
   CONSTRAINT     \   ALTER TABLE ONLY public.interactions
    ADD CONSTRAINT interactions_pkey PRIMARY KEY (id);
 H   ALTER TABLE ONLY public.interactions DROP CONSTRAINT interactions_pkey;
       public         postgres    false    215                       2606    224243    lifetimes lifetimes_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY public.lifetimes
    ADD CONSTRAINT lifetimes_pkey PRIMARY KEY (id);
 B   ALTER TABLE ONLY public.lifetimes DROP CONSTRAINT lifetimes_pkey;
       public         postgres    false    224            �
           2606    224120    medias medias_pkey 
   CONSTRAINT     P   ALTER TABLE ONLY public.medias
    ADD CONSTRAINT medias_pkey PRIMARY KEY (id);
 <   ALTER TABLE ONLY public.medias DROP CONSTRAINT medias_pkey;
       public         postgres    false    199                       2606    224182    names names_pkey 
   CONSTRAINT     N   ALTER TABLE ONLY public.names
    ADD CONSTRAINT names_pkey PRIMARY KEY (id);
 :   ALTER TABLE ONLY public.names DROP CONSTRAINT names_pkey;
       public         postgres    false    211            �
           2606    224125    nodes nodes_pkey 
   CONSTRAINT     N   ALTER TABLE ONLY public.nodes
    ADD CONSTRAINT nodes_pkey PRIMARY KEY (id);
 :   ALTER TABLE ONLY public.nodes DROP CONSTRAINT nodes_pkey;
       public         postgres    false    200                       2606    224262     patchelements patchelements_pkey 
   CONSTRAINT     ^   ALTER TABLE ONLY public.patchelements
    ADD CONSTRAINT patchelements_pkey PRIMARY KEY (id);
 J   ALTER TABLE ONLY public.patchelements DROP CONSTRAINT patchelements_pkey;
       public         postgres    false    228                       2606    224254    patches patches_pkey 
   CONSTRAINT     R   ALTER TABLE ONLY public.patches
    ADD CONSTRAINT patches_pkey PRIMARY KEY (id);
 >   ALTER TABLE ONLY public.patches DROP CONSTRAINT patches_pkey;
       public         postgres    false    226                       2606    224267    placements placements_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY public.placements
    ADD CONSTRAINT placements_pkey PRIMARY KEY (id);
 D   ALTER TABLE ONLY public.placements DROP CONSTRAINT placements_pkey;
       public         postgres    false    229            �
           2606    224140    plants plants_pkey 
   CONSTRAINT     P   ALTER TABLE ONLY public.plants
    ADD CONSTRAINT plants_pkey PRIMARY KEY (id);
 <   ALTER TABLE ONLY public.plants DROP CONSTRAINT plants_pkey;
       public         postgres    false    203            �
           2606    224145 .   precultures_mappping precultures_mappping_pkey 
   CONSTRAINT     |   ALTER TABLE ONLY public.precultures_mappping
    ADD CONSTRAINT precultures_mappping_pkey PRIMARY KEY (id, precultures_id);
 X   ALTER TABLE ONLY public.precultures_mappping DROP CONSTRAINT precultures_mappping_pkey;
       public         postgres    false    204    204                       2606    224171    predicates predicates_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY public.predicates
    ADD CONSTRAINT predicates_pkey PRIMARY KEY (id);
 D   ALTER TABLE ONLY public.predicates DROP CONSTRAINT predicates_pkey;
       public         postgres    false    209                       2606    224223     seedmaturitys seedmaturitys_pkey 
   CONSTRAINT     ^   ALTER TABLE ONLY public.seedmaturitys
    ADD CONSTRAINT seedmaturitys_pkey PRIMARY KEY (id);
 J   ALTER TABLE ONLY public.seedmaturitys DROP CONSTRAINT seedmaturitys_pkey;
       public         postgres    false    220                       2606    224213    sowings sowings_pkey 
   CONSTRAINT     R   ALTER TABLE ONLY public.sowings
    ADD CONSTRAINT sowings_pkey PRIMARY KEY (id);
 >   ALTER TABLE ONLY public.sowings DROP CONSTRAINT sowings_pkey;
       public         postgres    false    218            �
           2606    224135    species species_pkey 
   CONSTRAINT     R   ALTER TABLE ONLY public.species
    ADD CONSTRAINT species_pkey PRIMARY KEY (id);
 >   ALTER TABLE ONLY public.species DROP CONSTRAINT species_pkey;
       public         postgres    false    202            �
           2606    224109    subjects subjects_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY public.subjects
    ADD CONSTRAINT subjects_pkey PRIMARY KEY (id);
 @   ALTER TABLE ONLY public.subjects DROP CONSTRAINT subjects_pkey;
       public         postgres    false    197            �
           2606    224160    taxa taxa_pkey 
   CONSTRAINT     L   ALTER TABLE ONLY public.taxa
    ADD CONSTRAINT taxa_pkey PRIMARY KEY (id);
 8   ALTER TABLE ONLY public.taxa DROP CONSTRAINT taxa_pkey;
       public         postgres    false    207            	           2606    224208 "   timeperiods timeperiods_nextid_key 
   CONSTRAINT     _   ALTER TABLE ONLY public.timeperiods
    ADD CONSTRAINT timeperiods_nextid_key UNIQUE (nextid);
 L   ALTER TABLE ONLY public.timeperiods DROP CONSTRAINT timeperiods_nextid_key;
       public         postgres    false    217                       2606    224206    timeperiods timeperiods_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public.timeperiods
    ADD CONSTRAINT timeperiods_pkey PRIMARY KEY (id);
 F   ALTER TABLE ONLY public.timeperiods DROP CONSTRAINT timeperiods_pkey;
       public         postgres    false    217            "           2606    224278    nodes fk_1799fca2    FK CONSTRAINT     o   ALTER TABLE ONLY public.nodes
    ADD CONSTRAINT fk_1799fca2 FOREIGN KEY (parent) REFERENCES public.nodes(id);
 ;   ALTER TABLE ONLY public.nodes DROP CONSTRAINT fk_1799fca2;
       public       postgres    false    200    200    2801                        2606    224268    medias fk_194d2dde    FK CONSTRAINT     t   ALTER TABLE ONLY public.medias
    ADD CONSTRAINT fk_194d2dde FOREIGN KEY (subject) REFERENCES public.subjects(id);
 <   ALTER TABLE ONLY public.medias DROP CONSTRAINT fk_194d2dde;
       public       postgres    false    2797    199    197            -           2606    224333    names fk_2818358a    FK CONSTRAINT     m   ALTER TABLE ONLY public.names
    ADD CONSTRAINT fk_2818358a FOREIGN KEY (node) REFERENCES public.nodes(id);
 ;   ALTER TABLE ONLY public.names DROP CONSTRAINT fk_2818358a;
       public       postgres    false    2801    211    200            (           2606    224308 "   aftercultures_mappping fk_35f66856    FK CONSTRAINT     �   ALTER TABLE ONLY public.aftercultures_mappping
    ADD CONSTRAINT fk_35f66856 FOREIGN KEY (aftercultures_id) REFERENCES public.plants(id);
 L   ALTER TABLE ONLY public.aftercultures_mappping DROP CONSTRAINT fk_35f66856;
       public       postgres    false    203    205    2807            =           2606    224413    placements fk_42f126f5    FK CONSTRAINT     w   ALTER TABLE ONLY public.placements
    ADD CONSTRAINT fk_42f126f5 FOREIGN KEY (plantref) REFERENCES public.plants(id);
 @   ALTER TABLE ONLY public.placements DROP CONSTRAINT fk_42f126f5;
       public       postgres    false    203    2807    229            3           2606    224363    timeperiods fk_43a3d0a4    FK CONSTRAINT     |   ALTER TABLE ONLY public.timeperiods
    ADD CONSTRAINT fk_43a3d0a4 FOREIGN KEY (subjectref) REFERENCES public.subjects(id);
 A   ALTER TABLE ONLY public.timeperiods DROP CONSTRAINT fk_43a3d0a4;
       public       postgres    false    2797    197    217            /           2606    224343    interactions fk_43e8ec37    FK CONSTRAINT     ~   ALTER TABLE ONLY public.interactions
    ADD CONSTRAINT fk_43e8ec37 FOREIGN KEY (predicate) REFERENCES public.predicates(id);
 B   ALTER TABLE ONLY public.interactions DROP CONSTRAINT fk_43e8ec37;
       public       postgres    false    2817    215    209            .           2606    224338    interactions fk_4d48dc2f    FK CONSTRAINT     z   ALTER TABLE ONLY public.interactions
    ADD CONSTRAINT fk_4d48dc2f FOREIGN KEY (subject) REFERENCES public.subjects(id);
 B   ALTER TABLE ONLY public.interactions DROP CONSTRAINT fk_4d48dc2f;
       public       postgres    false    2797    215    197            %           2606    224293    plants fk_4fc23e80    FK CONSTRAINT     n   ALTER TABLE ONLY public.plants
    ADD CONSTRAINT fk_4fc23e80 FOREIGN KEY (id) REFERENCES public.species(id);
 <   ALTER TABLE ONLY public.plants DROP CONSTRAINT fk_4fc23e80;
       public       postgres    false    203    202    2805            ;           2606    224403    patchelements fk_52810082    FK CONSTRAINT     {   ALTER TABLE ONLY public.patchelements
    ADD CONSTRAINT fk_52810082 FOREIGN KEY (patchref) REFERENCES public.patches(id);
 C   ALTER TABLE ONLY public.patchelements DROP CONSTRAINT fk_52810082;
       public       postgres    false    228    226    2843            6           2606    224378    seedmaturitys fk_551a11ff    FK CONSTRAINT     y   ALTER TABLE ONLY public.seedmaturitys
    ADD CONSTRAINT fk_551a11ff FOREIGN KEY (id) REFERENCES public.timeperiods(id);
 C   ALTER TABLE ONLY public.seedmaturitys DROP CONSTRAINT fk_551a11ff;
       public       postgres    false    217    2827    220            <           2606    224408    placements fk_58a4e232    FK CONSTRAINT     x   ALTER TABLE ONLY public.placements
    ADD CONSTRAINT fk_58a4e232 FOREIGN KEY (id) REFERENCES public.patchelements(id);
 @   ALTER TABLE ONLY public.placements DROP CONSTRAINT fk_58a4e232;
       public       postgres    false    229    2845    228            8           2606    224388    cultivates fk_6db50684    FK CONSTRAINT     v   ALTER TABLE ONLY public.cultivates
    ADD CONSTRAINT fk_6db50684 FOREIGN KEY (id) REFERENCES public.timeperiods(id);
 @   ALTER TABLE ONLY public.cultivates DROP CONSTRAINT fk_6db50684;
       public       postgres    false    222    217    2827            $           2606    224288    species fk_6df987d9    FK CONSTRAINT     m   ALTER TABLE ONLY public.species
    ADD CONSTRAINT fk_6df987d9 FOREIGN KEY (id) REFERENCES public.nodes(id);
 =   ALTER TABLE ONLY public.species DROP CONSTRAINT fk_6df987d9;
       public       postgres    false    2801    200    202            *           2606    224318    animals fk_84bf138b    FK CONSTRAINT     o   ALTER TABLE ONLY public.animals
    ADD CONSTRAINT fk_84bf138b FOREIGN KEY (id) REFERENCES public.species(id);
 =   ALTER TABLE ONLY public.animals DROP CONSTRAINT fk_84bf138b;
       public       postgres    false    206    2805    202            +           2606    224323    taxa fk_977b2c4b    FK CONSTRAINT     j   ALTER TABLE ONLY public.taxa
    ADD CONSTRAINT fk_977b2c4b FOREIGN KEY (id) REFERENCES public.nodes(id);
 :   ALTER TABLE ONLY public.taxa DROP CONSTRAINT fk_977b2c4b;
       public       postgres    false    207    2801    200            5           2606    224373    harvests fk_9a9122eb    FK CONSTRAINT     t   ALTER TABLE ONLY public.harvests
    ADD CONSTRAINT fk_9a9122eb FOREIGN KEY (id) REFERENCES public.timeperiods(id);
 >   ALTER TABLE ONLY public.harvests DROP CONSTRAINT fk_9a9122eb;
       public       postgres    false    217    219    2827            #           2606    224283    effects fk_b1b33f7a    FK CONSTRAINT     m   ALTER TABLE ONLY public.effects
    ADD CONSTRAINT fk_b1b33f7a FOREIGN KEY (id) REFERENCES public.nodes(id);
 =   ALTER TABLE ONLY public.effects DROP CONSTRAINT fk_b1b33f7a;
       public       postgres    false    201    200    2801            &           2606    224298     precultures_mappping fk_b2fb0a2b    FK CONSTRAINT     �   ALTER TABLE ONLY public.precultures_mappping
    ADD CONSTRAINT fk_b2fb0a2b FOREIGN KEY (precultures_id) REFERENCES public.plants(id);
 J   ALTER TABLE ONLY public.precultures_mappping DROP CONSTRAINT fk_b2fb0a2b;
       public       postgres    false    2807    203    204            ,           2606    224328    predicates fk_c7ecb262    FK CONSTRAINT     |   ALTER TABLE ONLY public.predicates
    ADD CONSTRAINT fk_c7ecb262 FOREIGN KEY (parentref) REFERENCES public.predicates(id);
 @   ALTER TABLE ONLY public.predicates DROP CONSTRAINT fk_c7ecb262;
       public       postgres    false    209    2817    209            7           2606    224383    blooms fk_cffa174f    FK CONSTRAINT     r   ALTER TABLE ONLY public.blooms
    ADD CONSTRAINT fk_cffa174f FOREIGN KEY (id) REFERENCES public.timeperiods(id);
 <   ALTER TABLE ONLY public.blooms DROP CONSTRAINT fk_cffa174f;
       public       postgres    false    221    217    2827            )           2606    224313 "   aftercultures_mappping fk_d5beeecf    FK CONSTRAINT     }   ALTER TABLE ONLY public.aftercultures_mappping
    ADD CONSTRAINT fk_d5beeecf FOREIGN KEY (id) REFERENCES public.plants(id);
 L   ALTER TABLE ONLY public.aftercultures_mappping DROP CONSTRAINT fk_d5beeecf;
       public       postgres    false    205    2807    203            :           2606    224398    lifetimes fk_e0aa9f58    FK CONSTRAINT     u   ALTER TABLE ONLY public.lifetimes
    ADD CONSTRAINT fk_e0aa9f58 FOREIGN KEY (id) REFERENCES public.timeperiods(id);
 ?   ALTER TABLE ONLY public.lifetimes DROP CONSTRAINT fk_e0aa9f58;
       public       postgres    false    224    217    2827            0           2606    224348    interactions fk_e0ba4134    FK CONSTRAINT     y   ALTER TABLE ONLY public.interactions
    ADD CONSTRAINT fk_e0ba4134 FOREIGN KEY (object) REFERENCES public.subjects(id);
 B   ALTER TABLE ONLY public.interactions DROP CONSTRAINT fk_e0ba4134;
       public       postgres    false    215    2797    197            '           2606    224303     precultures_mappping fk_e492aee8    FK CONSTRAINT     {   ALTER TABLE ONLY public.precultures_mappping
    ADD CONSTRAINT fk_e492aee8 FOREIGN KEY (id) REFERENCES public.plants(id);
 J   ALTER TABLE ONLY public.precultures_mappping DROP CONSTRAINT fk_e492aee8;
       public       postgres    false    204    203    2807            4           2606    224368    sowings fk_e9af2ae    FK CONSTRAINT     r   ALTER TABLE ONLY public.sowings
    ADD CONSTRAINT fk_e9af2ae FOREIGN KEY (id) REFERENCES public.timeperiods(id);
 <   ALTER TABLE ONLY public.sowings DROP CONSTRAINT fk_e9af2ae;
       public       postgres    false    217    218    2827            2           2606    224358    timeperiods fk_f16d7073    FK CONSTRAINT     {   ALTER TABLE ONLY public.timeperiods
    ADD CONSTRAINT fk_f16d7073 FOREIGN KEY (nextid) REFERENCES public.timeperiods(id);
 A   ALTER TABLE ONLY public.timeperiods DROP CONSTRAINT fk_f16d7073;
       public       postgres    false    217    217    2827            1           2606    224353    interactions fk_f34fd50e    FK CONSTRAINT     �   ALTER TABLE ONLY public.interactions
    ADD CONSTRAINT fk_f34fd50e FOREIGN KEY (impactsubject) REFERENCES public.subjects(id);
 B   ALTER TABLE ONLY public.interactions DROP CONSTRAINT fk_f34fd50e;
       public       postgres    false    197    215    2797            !           2606    224273    nodes fk_f44eadb2    FK CONSTRAINT     n   ALTER TABLE ONLY public.nodes
    ADD CONSTRAINT fk_f44eadb2 FOREIGN KEY (id) REFERENCES public.subjects(id);
 ;   ALTER TABLE ONLY public.nodes DROP CONSTRAINT fk_f44eadb2;
       public       postgres    false    200    197    2797            9           2606    224393    implants fk_fcf6ea49    FK CONSTRAINT     t   ALTER TABLE ONLY public.implants
    ADD CONSTRAINT fk_fcf6ea49 FOREIGN KEY (id) REFERENCES public.timeperiods(id);
 >   ALTER TABLE ONLY public.implants DROP CONSTRAINT fk_fcf6ea49;
       public       postgres    false    223    217    2827            �      x������ � �      �      x������ � �      �      x������ � �      �      x������ � �      �   O  x�5�[�� �=�I�%�_G��H�gԗA	=��jW�����\��|�k���{�|o���ǯ��n~@�k+P���;��̚���5߂x�O����.���b�.dg����Fg#��R��|��i��y����v'��ˑ������{\O�f������y]������7n�#٘�[q�x����9�:r���ۑ��H��!�M�7�� �� qRWbЭ��ȼu���q�ofsH6��o�n
�.6��o@>r�v���	º��ו�}͂�Z����v��{�f��'�FG � �Y��r̭����ٯдO�Y�����GM浬��6>]���s�g��>������ �}�G"�B�zUX�x�(�{krb�M�<��C�m�}�1<�����ۂ^�8�c���G�c�9!(��JEG��"|GV�"\���c1�.����LkJ<�4�:�l���xtG����"qw���p�Y�1��S}
�q�oh���oC�N	i�A�=:�Q)�AI�P� ��
��˜ ��܆z��}��D~�!��.Ҟ�P{JGGΣ�*�N�#�,�{�e!�y:3hHai��
S#��'4<��4\B�#j��1Ҏ�g�D%A�M�f��z�X���Q:��MFv�L��I�a� �DC�z\J� +-��:Nhr@zN��C+��$b$�I����$^�	�fz�z�]z�K�QQ#�r�h�-6�;	OJ���0�
�H� &Y�ESmb�1�湡iP���zs`�75���jIW��Ӌ(�O�sZwf���
�p[����H��C�dJ�����l-%C��i3x�ש�!��W��ALZ3h%ūAûv��c�,e���įBl"z��t-��}W:;׆��PK�{?�$���-r��.K䳠�t�Dd�K��6��6�*TZ&�,5�;�!2w�6[��h$@�J�M�/�Xs�f�Uqb����ۻ���2�Z��C��$o�r�m��,�I��ek��Oa..�Hť�P���X
b�}�{A�u��y7��H�^dEz
m���[�,��B�O0��A�R$��׈�`K�{" ��؂f�Qc6@��)�2��B�o/Ī�l��V�]�I{��e豝�F�%h�]�v��������y#�vo�J��R{�2�U��ZfϨ�⌦Kn���{�NC
Z�F�&C�ӥe��R�8��1���c�K��ܫh��~]���{Q�'�U����I$.�Ѳ,���-�%/���^&\@l��^ 醀8��f�fp����x���b��֟Q
F�����p������{�*�_��0��١���4bؽ��w�M���+j��~��##�      �      x������ � �      �      x������ � �      �      x������ � �      �      x������ � �      �      x������ � �      �      x������ � �      �      x������ � �      �      x������ � �      �      x������ � �      �      x������ � �      �      x������ � �      �      x������ � �      �      x������ � �      �   �   x�3�,�/�,�,�,��/Q�UH/-���2��KMOD/N��IM� Ksy���T�prr�p&��ޓW\��0�,�C2�2�L;��(%����3�(��0�lؒ�JKΌ���Լ'F��� 63�      �      x������ � �      �      x������ � �      �      x������ � �      �      x������ � �      �      x������ � �      �      x������ � �     