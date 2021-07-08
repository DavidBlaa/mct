PGDMP         '                y           mct    10.11    10.11 �               0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                       false                       0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                       false                       0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                       false                       1262    220586    mct    DATABASE     �   CREATE DATABASE mct WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'German_Germany.1252' LC_CTYPE = 'German_Germany.1252';
    DROP DATABASE mct;
             postgres    false                        2615    2200    public    SCHEMA        CREATE SCHEMA public;
    DROP SCHEMA public;
             postgres    false                       0    0    SCHEMA public    COMMENT     6   COMMENT ON SCHEMA public IS 'standard public schema';
                  postgres    false    3                        3079    12924    plpgsql 	   EXTENSION     ?   CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;
    DROP EXTENSION plpgsql;
                  false                       0    0    EXTENSION plpgsql    COMMENT     @   COMMENT ON EXTENSION plpgsql IS 'PL/pgSQL procedural language';
                       false    1            �            1259    220587    aftercultures_mappping    TABLE     m   CREATE TABLE public.aftercultures_mappping (
    id bigint NOT NULL,
    aftercultures_id bigint NOT NULL
);
 *   DROP TABLE public.aftercultures_mappping;
       public         postgres    false    3            �            1259    220590    animals    TABLE     8   CREATE TABLE public.animals (
    id bigint NOT NULL
);
    DROP TABLE public.animals;
       public         postgres    false    3            �            1259    220593    blooms    TABLE     7   CREATE TABLE public.blooms (
    id bigint NOT NULL
);
    DROP TABLE public.blooms;
       public         postgres    false    3            �            1259    220596 
   cultivates    TABLE     �   CREATE TABLE public.cultivates (
    id bigint NOT NULL,
    germinationperioddays integer,
    germinationtemperature double precision
);
    DROP TABLE public.cultivates;
       public         postgres    false    3            �            1259    220599    days    TABLE     e   CREATE TABLE public.days (
    id bigint NOT NULL,
    dayinyear integer,
    weekperyear integer
);
    DROP TABLE public.days;
       public         postgres    false    3            �            1259    220602    days_id_seq    SEQUENCE     t   CREATE SEQUENCE public.days_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 "   DROP SEQUENCE public.days_id_seq;
       public       postgres    false    3    200                       0    0    days_id_seq    SEQUENCE OWNED BY     ;   ALTER SEQUENCE public.days_id_seq OWNED BY public.days.id;
            public       postgres    false    201            �            1259    220604    effects    TABLE     8   CREATE TABLE public.effects (
    id bigint NOT NULL
);
    DROP TABLE public.effects;
       public         postgres    false    3            �            1259    220607    harvests    TABLE     9   CREATE TABLE public.harvests (
    id bigint NOT NULL
);
    DROP TABLE public.harvests;
       public         postgres    false    3            �            1259    220610    implants    TABLE     9   CREATE TABLE public.implants (
    id bigint NOT NULL
);
    DROP TABLE public.implants;
       public         postgres    false    3            �            1259    220613    interactions    TABLE     �   CREATE TABLE public.interactions (
    id bigint NOT NULL,
    subject bigint NOT NULL,
    predicate bigint NOT NULL,
    object bigint NOT NULL,
    impactsubject bigint,
    indicator integer
);
     DROP TABLE public.interactions;
       public         postgres    false    3            �            1259    220616    interactions_id_seq    SEQUENCE     |   CREATE SEQUENCE public.interactions_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 *   DROP SEQUENCE public.interactions_id_seq;
       public       postgres    false    3    205                       0    0    interactions_id_seq    SEQUENCE OWNED BY     K   ALTER SEQUENCE public.interactions_id_seq OWNED BY public.interactions.id;
            public       postgres    false    206            �            1259    220618 	   lifetimes    TABLE     :   CREATE TABLE public.lifetimes (
    id bigint NOT NULL
);
    DROP TABLE public.lifetimes;
       public         postgres    false    3            �            1259    220621    medias    TABLE     �   CREATE TABLE public.medias (
    id bigint NOT NULL,
    imagepath character varying(255),
    mimetype character varying(255),
    subject bigint
);
    DROP TABLE public.medias;
       public         postgres    false    3            �            1259    220627    medias_id_seq    SEQUENCE     v   CREATE SEQUENCE public.medias_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 $   DROP SEQUENCE public.medias_id_seq;
       public       postgres    false    3    208                       0    0    medias_id_seq    SEQUENCE OWNED BY     ?   ALTER SEQUENCE public.medias_id_seq OWNED BY public.medias.id;
            public       postgres    false    209            �            1259    220629    names    TABLE     �   CREATE TABLE public.names (
    id bigint NOT NULL,
    node bigint NOT NULL,
    name character varying(255),
    language character varying(255),
    ispreferredname boolean
);
    DROP TABLE public.names;
       public         postgres    false    3            �            1259    220635    names_id_seq    SEQUENCE     u   CREATE SEQUENCE public.names_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 #   DROP SEQUENCE public.names_id_seq;
       public       postgres    false    3    210                        0    0    names_id_seq    SEQUENCE OWNED BY     =   ALTER SEQUENCE public.names_id_seq OWNED BY public.names.id;
            public       postgres    false    211            �            1259    220637    nodes    TABLE     �   CREATE TABLE public.nodes (
    id bigint NOT NULL,
    scientificname character varying(255),
    rank integer,
    parent bigint
);
    DROP TABLE public.nodes;
       public         postgres    false    3            �            1259    220640    patchelements    TABLE     �   CREATE TABLE public.patchelements (
    id bigint NOT NULL,
    transformation character varying(255),
    patchref bigint NOT NULL
);
 !   DROP TABLE public.patchelements;
       public         postgres    false    3            �            1259    220643    patchelements_id_seq    SEQUENCE     }   CREATE SEQUENCE public.patchelements_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 +   DROP SEQUENCE public.patchelements_id_seq;
       public       postgres    false    3    213            !           0    0    patchelements_id_seq    SEQUENCE OWNED BY     M   ALTER SEQUENCE public.patchelements_id_seq OWNED BY public.patchelements.id;
            public       postgres    false    214            �            1259    220645    patches    TABLE     �   CREATE TABLE public.patches (
    id bigint NOT NULL,
    name character varying(255),
    description character varying(3000),
    width integer,
    height integer,
    locationtype integer,
    nutrientclaim integer
);
    DROP TABLE public.patches;
       public         postgres    false    3            �            1259    220651    patches_id_seq    SEQUENCE     w   CREATE SEQUENCE public.patches_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 %   DROP SEQUENCE public.patches_id_seq;
       public       postgres    false    3    215            "           0    0    patches_id_seq    SEQUENCE OWNED BY     A   ALTER SEQUENCE public.patches_id_seq OWNED BY public.patches.id;
            public       postgres    false    216            �            1259    220653 
   placements    TABLE     �   CREATE TABLE public.placements (
    id bigint NOT NULL,
    plantingarea integer,
    plantingmonth integer,
    plantref bigint
);
    DROP TABLE public.placements;
       public         postgres    false    3            �            1259    220656    plants    TABLE     �   CREATE TABLE public.plants (
    id bigint NOT NULL,
    width double precision,
    height double precision,
    rootdepth integer,
    nutrientclaim integer,
    locationtype integer,
    sowingdepth integer
);
    DROP TABLE public.plants;
       public         postgres    false    3            �            1259    220659    precultures_mappping    TABLE     i   CREATE TABLE public.precultures_mappping (
    id bigint NOT NULL,
    precultures_id bigint NOT NULL
);
 (   DROP TABLE public.precultures_mappping;
       public         postgres    false    3            �            1259    220662 
   predicates    TABLE     �   CREATE TABLE public.predicates (
    id bigint NOT NULL,
    name character varying(255),
    description character varying(255),
    parentref bigint
);
    DROP TABLE public.predicates;
       public         postgres    false    3            �            1259    220668    predicates_id_seq    SEQUENCE     z   CREATE SEQUENCE public.predicates_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 (   DROP SEQUENCE public.predicates_id_seq;
       public       postgres    false    3    220            #           0    0    predicates_id_seq    SEQUENCE OWNED BY     G   ALTER SEQUENCE public.predicates_id_seq OWNED BY public.predicates.id;
            public       postgres    false    221            �            1259    220670    seedmaturitys    TABLE     >   CREATE TABLE public.seedmaturitys (
    id bigint NOT NULL
);
 !   DROP TABLE public.seedmaturitys;
       public         postgres    false    3            �            1259    220673    sowings    TABLE     8   CREATE TABLE public.sowings (
    id bigint NOT NULL
);
    DROP TABLE public.sowings;
       public         postgres    false    3            �            1259    220676    species    TABLE     8   CREATE TABLE public.species (
    id bigint NOT NULL
);
    DROP TABLE public.species;
       public         postgres    false    3            �            1259    220679    subjects    TABLE     �   CREATE TABLE public.subjects (
    id bigint NOT NULL,
    name character varying(255),
    description character varying(3000)
);
    DROP TABLE public.subjects;
       public         postgres    false    3            �            1259    220685    subjects_id_seq    SEQUENCE     x   CREATE SEQUENCE public.subjects_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 &   DROP SEQUENCE public.subjects_id_seq;
       public       postgres    false    225    3            $           0    0    subjects_id_seq    SEQUENCE OWNED BY     C   ALTER SEQUENCE public.subjects_id_seq OWNED BY public.subjects.id;
            public       postgres    false    226            �            1259    220687    taxa    TABLE     5   CREATE TABLE public.taxa (
    id bigint NOT NULL
);
    DROP TABLE public.taxa;
       public         postgres    false    3            �            1259    220690    timeperiods    TABLE     �   CREATE TABLE public.timeperiods (
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
       public         postgres    false    3            �            1259    220693    timeperiods_id_seq    SEQUENCE     {   CREATE SEQUENCE public.timeperiods_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 )   DROP SEQUENCE public.timeperiods_id_seq;
       public       postgres    false    228    3            %           0    0    timeperiods_id_seq    SEQUENCE OWNED BY     I   ALTER SEQUENCE public.timeperiods_id_seq OWNED BY public.timeperiods.id;
            public       postgres    false    229            �
           2604    220695    days id    DEFAULT     b   ALTER TABLE ONLY public.days ALTER COLUMN id SET DEFAULT nextval('public.days_id_seq'::regclass);
 6   ALTER TABLE public.days ALTER COLUMN id DROP DEFAULT;
       public       postgres    false    201    200            �
           2604    220696    interactions id    DEFAULT     r   ALTER TABLE ONLY public.interactions ALTER COLUMN id SET DEFAULT nextval('public.interactions_id_seq'::regclass);
 >   ALTER TABLE public.interactions ALTER COLUMN id DROP DEFAULT;
       public       postgres    false    206    205            �
           2604    220697 	   medias id    DEFAULT     f   ALTER TABLE ONLY public.medias ALTER COLUMN id SET DEFAULT nextval('public.medias_id_seq'::regclass);
 8   ALTER TABLE public.medias ALTER COLUMN id DROP DEFAULT;
       public       postgres    false    209    208            �
           2604    220698    names id    DEFAULT     d   ALTER TABLE ONLY public.names ALTER COLUMN id SET DEFAULT nextval('public.names_id_seq'::regclass);
 7   ALTER TABLE public.names ALTER COLUMN id DROP DEFAULT;
       public       postgres    false    211    210            �
           2604    220699    patchelements id    DEFAULT     t   ALTER TABLE ONLY public.patchelements ALTER COLUMN id SET DEFAULT nextval('public.patchelements_id_seq'::regclass);
 ?   ALTER TABLE public.patchelements ALTER COLUMN id DROP DEFAULT;
       public       postgres    false    214    213            �
           2604    220700 
   patches id    DEFAULT     h   ALTER TABLE ONLY public.patches ALTER COLUMN id SET DEFAULT nextval('public.patches_id_seq'::regclass);
 9   ALTER TABLE public.patches ALTER COLUMN id DROP DEFAULT;
       public       postgres    false    216    215            �
           2604    220701    predicates id    DEFAULT     n   ALTER TABLE ONLY public.predicates ALTER COLUMN id SET DEFAULT nextval('public.predicates_id_seq'::regclass);
 <   ALTER TABLE public.predicates ALTER COLUMN id DROP DEFAULT;
       public       postgres    false    221    220            �
           2604    220702    subjects id    DEFAULT     j   ALTER TABLE ONLY public.subjects ALTER COLUMN id SET DEFAULT nextval('public.subjects_id_seq'::regclass);
 :   ALTER TABLE public.subjects ALTER COLUMN id DROP DEFAULT;
       public       postgres    false    226    225            �
           2604    220703    timeperiods id    DEFAULT     p   ALTER TABLE ONLY public.timeperiods ALTER COLUMN id SET DEFAULT nextval('public.timeperiods_id_seq'::regclass);
 =   ALTER TABLE public.timeperiods ALTER COLUMN id DROP DEFAULT;
       public       postgres    false    229    228            �          0    220587    aftercultures_mappping 
   TABLE DATA               F   COPY public.aftercultures_mappping (id, aftercultures_id) FROM stdin;
    public       postgres    false    196   ��       �          0    220590    animals 
   TABLE DATA               %   COPY public.animals (id) FROM stdin;
    public       postgres    false    197   ��       �          0    220593    blooms 
   TABLE DATA               $   COPY public.blooms (id) FROM stdin;
    public       postgres    false    198   �       �          0    220596 
   cultivates 
   TABLE DATA               W   COPY public.cultivates (id, germinationperioddays, germinationtemperature) FROM stdin;
    public       postgres    false    199   *�       �          0    220599    days 
   TABLE DATA               :   COPY public.days (id, dayinyear, weekperyear) FROM stdin;
    public       postgres    false    200   c�       �          0    220604    effects 
   TABLE DATA               %   COPY public.effects (id) FROM stdin;
    public       postgres    false    202   �       �          0    220607    harvests 
   TABLE DATA               &   COPY public.harvests (id) FROM stdin;
    public       postgres    false    203   	      �          0    220610    implants 
   TABLE DATA               &   COPY public.implants (id) FROM stdin;
    public       postgres    false    204   D      �          0    220613    interactions 
   TABLE DATA               `   COPY public.interactions (id, subject, predicate, object, impactsubject, indicator) FROM stdin;
    public       postgres    false    205   y      �          0    220618 	   lifetimes 
   TABLE DATA               '   COPY public.lifetimes (id) FROM stdin;
    public       postgres    false    207   =      �          0    220621    medias 
   TABLE DATA               B   COPY public.medias (id, imagepath, mimetype, subject) FROM stdin;
    public       postgres    false    208   Z                0    220629    names 
   TABLE DATA               J   COPY public.names (id, node, name, language, ispreferredname) FROM stdin;
    public       postgres    false    210   �                0    220637    nodes 
   TABLE DATA               A   COPY public.nodes (id, scientificname, rank, parent) FROM stdin;
    public       postgres    false    212   �                0    220640    patchelements 
   TABLE DATA               E   COPY public.patchelements (id, transformation, patchref) FROM stdin;
    public       postgres    false    213                   0    220645    patches 
   TABLE DATA               d   COPY public.patches (id, name, description, width, height, locationtype, nutrientclaim) FROM stdin;
    public       postgres    false    215   %                0    220653 
   placements 
   TABLE DATA               O   COPY public.placements (id, plantingarea, plantingmonth, plantref) FROM stdin;
    public       postgres    false    217   n      	          0    220656    plants 
   TABLE DATA               h   COPY public.plants (id, width, height, rootdepth, nutrientclaim, locationtype, sowingdepth) FROM stdin;
    public       postgres    false    218   �      
          0    220659    precultures_mappping 
   TABLE DATA               B   COPY public.precultures_mappping (id, precultures_id) FROM stdin;
    public       postgres    false    219   !                0    220662 
   predicates 
   TABLE DATA               F   COPY public.predicates (id, name, description, parentref) FROM stdin;
    public       postgres    false    220   :!                0    220670    seedmaturitys 
   TABLE DATA               +   COPY public.seedmaturitys (id) FROM stdin;
    public       postgres    false    222   �!                0    220673    sowings 
   TABLE DATA               %   COPY public.sowings (id) FROM stdin;
    public       postgres    false    223   �!                0    220676    species 
   TABLE DATA               %   COPY public.species (id) FROM stdin;
    public       postgres    false    224   "                0    220679    subjects 
   TABLE DATA               9   COPY public.subjects (id, name, description) FROM stdin;
    public       postgres    false    225   �#                0    220687    taxa 
   TABLE DATA               "   COPY public.taxa (id) FROM stdin;
    public       postgres    false    227   5                0    220690    timeperiods 
   TABLE DATA               n   COPY public.timeperiods (id, startarea, startmonth, endarea, endmonth, start, nextid, subjectref) FROM stdin;
    public       postgres    false    228   �5      &           0    0    days_id_seq    SEQUENCE SET     ;   SELECT pg_catalog.setval('public.days_id_seq', 364, true);
            public       postgres    false    201            '           0    0    interactions_id_seq    SEQUENCE SET     C   SELECT pg_catalog.setval('public.interactions_id_seq', 883, true);
            public       postgres    false    206            (           0    0    medias_id_seq    SEQUENCE SET     <   SELECT pg_catalog.setval('public.medias_id_seq', 45, true);
            public       postgres    false    209            )           0    0    names_id_seq    SEQUENCE SET     ;   SELECT pg_catalog.setval('public.names_id_seq', 1, false);
            public       postgres    false    211            *           0    0    patchelements_id_seq    SEQUENCE SET     C   SELECT pg_catalog.setval('public.patchelements_id_seq', 1, false);
            public       postgres    false    214            +           0    0    patches_id_seq    SEQUENCE SET     <   SELECT pg_catalog.setval('public.patches_id_seq', 2, true);
            public       postgres    false    216            ,           0    0    predicates_id_seq    SEQUENCE SET     @   SELECT pg_catalog.setval('public.predicates_id_seq', 10, true);
            public       postgres    false    221            -           0    0    subjects_id_seq    SEQUENCE SET     ?   SELECT pg_catalog.setval('public.subjects_id_seq', 364, true);
            public       postgres    false    226            .           0    0    timeperiods_id_seq    SEQUENCE SET     A   SELECT pg_catalog.setval('public.timeperiods_id_seq', 36, true);
            public       postgres    false    229            �
           2606    220705 2   aftercultures_mappping aftercultures_mappping_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public.aftercultures_mappping
    ADD CONSTRAINT aftercultures_mappping_pkey PRIMARY KEY (id, aftercultures_id);
 \   ALTER TABLE ONLY public.aftercultures_mappping DROP CONSTRAINT aftercultures_mappping_pkey;
       public         postgres    false    196    196            �
           2606    220707    animals animals_pkey 
   CONSTRAINT     R   ALTER TABLE ONLY public.animals
    ADD CONSTRAINT animals_pkey PRIMARY KEY (id);
 >   ALTER TABLE ONLY public.animals DROP CONSTRAINT animals_pkey;
       public         postgres    false    197            �
           2606    220709    blooms blooms_pkey 
   CONSTRAINT     P   ALTER TABLE ONLY public.blooms
    ADD CONSTRAINT blooms_pkey PRIMARY KEY (id);
 <   ALTER TABLE ONLY public.blooms DROP CONSTRAINT blooms_pkey;
       public         postgres    false    198            �
           2606    220711    cultivates cultivates_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY public.cultivates
    ADD CONSTRAINT cultivates_pkey PRIMARY KEY (id);
 D   ALTER TABLE ONLY public.cultivates DROP CONSTRAINT cultivates_pkey;
       public         postgres    false    199            �
           2606    220713    days days_pkey 
   CONSTRAINT     L   ALTER TABLE ONLY public.days
    ADD CONSTRAINT days_pkey PRIMARY KEY (id);
 8   ALTER TABLE ONLY public.days DROP CONSTRAINT days_pkey;
       public         postgres    false    200            �
           2606    220715    effects effects_pkey 
   CONSTRAINT     R   ALTER TABLE ONLY public.effects
    ADD CONSTRAINT effects_pkey PRIMARY KEY (id);
 >   ALTER TABLE ONLY public.effects DROP CONSTRAINT effects_pkey;
       public         postgres    false    202            �
           2606    220717    harvests harvests_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY public.harvests
    ADD CONSTRAINT harvests_pkey PRIMARY KEY (id);
 @   ALTER TABLE ONLY public.harvests DROP CONSTRAINT harvests_pkey;
       public         postgres    false    203            �
           2606    220719    implants implants_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY public.implants
    ADD CONSTRAINT implants_pkey PRIMARY KEY (id);
 @   ALTER TABLE ONLY public.implants DROP CONSTRAINT implants_pkey;
       public         postgres    false    204            �
           2606    220721    interactions interactions_pkey 
   CONSTRAINT     \   ALTER TABLE ONLY public.interactions
    ADD CONSTRAINT interactions_pkey PRIMARY KEY (id);
 H   ALTER TABLE ONLY public.interactions DROP CONSTRAINT interactions_pkey;
       public         postgres    false    205            �
           2606    220723    lifetimes lifetimes_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY public.lifetimes
    ADD CONSTRAINT lifetimes_pkey PRIMARY KEY (id);
 B   ALTER TABLE ONLY public.lifetimes DROP CONSTRAINT lifetimes_pkey;
       public         postgres    false    207                       2606    220725    medias medias_pkey 
   CONSTRAINT     P   ALTER TABLE ONLY public.medias
    ADD CONSTRAINT medias_pkey PRIMARY KEY (id);
 <   ALTER TABLE ONLY public.medias DROP CONSTRAINT medias_pkey;
       public         postgres    false    208                       2606    220727    names names_pkey 
   CONSTRAINT     N   ALTER TABLE ONLY public.names
    ADD CONSTRAINT names_pkey PRIMARY KEY (id);
 :   ALTER TABLE ONLY public.names DROP CONSTRAINT names_pkey;
       public         postgres    false    210                       2606    220729    nodes nodes_pkey 
   CONSTRAINT     N   ALTER TABLE ONLY public.nodes
    ADD CONSTRAINT nodes_pkey PRIMARY KEY (id);
 :   ALTER TABLE ONLY public.nodes DROP CONSTRAINT nodes_pkey;
       public         postgres    false    212                       2606    220731     patchelements patchelements_pkey 
   CONSTRAINT     ^   ALTER TABLE ONLY public.patchelements
    ADD CONSTRAINT patchelements_pkey PRIMARY KEY (id);
 J   ALTER TABLE ONLY public.patchelements DROP CONSTRAINT patchelements_pkey;
       public         postgres    false    213            	           2606    220733    patches patches_pkey 
   CONSTRAINT     R   ALTER TABLE ONLY public.patches
    ADD CONSTRAINT patches_pkey PRIMARY KEY (id);
 >   ALTER TABLE ONLY public.patches DROP CONSTRAINT patches_pkey;
       public         postgres    false    215                       2606    220735    placements placements_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY public.placements
    ADD CONSTRAINT placements_pkey PRIMARY KEY (id);
 D   ALTER TABLE ONLY public.placements DROP CONSTRAINT placements_pkey;
       public         postgres    false    217                       2606    220737    plants plants_pkey 
   CONSTRAINT     P   ALTER TABLE ONLY public.plants
    ADD CONSTRAINT plants_pkey PRIMARY KEY (id);
 <   ALTER TABLE ONLY public.plants DROP CONSTRAINT plants_pkey;
       public         postgres    false    218                       2606    220739 .   precultures_mappping precultures_mappping_pkey 
   CONSTRAINT     |   ALTER TABLE ONLY public.precultures_mappping
    ADD CONSTRAINT precultures_mappping_pkey PRIMARY KEY (id, precultures_id);
 X   ALTER TABLE ONLY public.precultures_mappping DROP CONSTRAINT precultures_mappping_pkey;
       public         postgres    false    219    219                       2606    220741    predicates predicates_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY public.predicates
    ADD CONSTRAINT predicates_pkey PRIMARY KEY (id);
 D   ALTER TABLE ONLY public.predicates DROP CONSTRAINT predicates_pkey;
       public         postgres    false    220                       2606    220743     seedmaturitys seedmaturitys_pkey 
   CONSTRAINT     ^   ALTER TABLE ONLY public.seedmaturitys
    ADD CONSTRAINT seedmaturitys_pkey PRIMARY KEY (id);
 J   ALTER TABLE ONLY public.seedmaturitys DROP CONSTRAINT seedmaturitys_pkey;
       public         postgres    false    222                       2606    220745    sowings sowings_pkey 
   CONSTRAINT     R   ALTER TABLE ONLY public.sowings
    ADD CONSTRAINT sowings_pkey PRIMARY KEY (id);
 >   ALTER TABLE ONLY public.sowings DROP CONSTRAINT sowings_pkey;
       public         postgres    false    223                       2606    220747    species species_pkey 
   CONSTRAINT     R   ALTER TABLE ONLY public.species
    ADD CONSTRAINT species_pkey PRIMARY KEY (id);
 >   ALTER TABLE ONLY public.species DROP CONSTRAINT species_pkey;
       public         postgres    false    224                       2606    220749    subjects subjects_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY public.subjects
    ADD CONSTRAINT subjects_pkey PRIMARY KEY (id);
 @   ALTER TABLE ONLY public.subjects DROP CONSTRAINT subjects_pkey;
       public         postgres    false    225                       2606    220751    taxa taxa_pkey 
   CONSTRAINT     L   ALTER TABLE ONLY public.taxa
    ADD CONSTRAINT taxa_pkey PRIMARY KEY (id);
 8   ALTER TABLE ONLY public.taxa DROP CONSTRAINT taxa_pkey;
       public         postgres    false    227                       2606    220753 "   timeperiods timeperiods_nextid_key 
   CONSTRAINT     _   ALTER TABLE ONLY public.timeperiods
    ADD CONSTRAINT timeperiods_nextid_key UNIQUE (nextid);
 L   ALTER TABLE ONLY public.timeperiods DROP CONSTRAINT timeperiods_nextid_key;
       public         postgres    false    228                       2606    220755    timeperiods timeperiods_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public.timeperiods
    ADD CONSTRAINT timeperiods_pkey PRIMARY KEY (id);
 F   ALTER TABLE ONLY public.timeperiods DROP CONSTRAINT timeperiods_pkey;
       public         postgres    false    228            ]           2606    220934    plants fk1788add739da9f19    FK CONSTRAINT     u   ALTER TABLE ONLY public.plants
    ADD CONSTRAINT fk1788add739da9f19 FOREIGN KEY (id) REFERENCES public.species(id);
 C   ALTER TABLE ONLY public.plants DROP CONSTRAINT fk1788add739da9f19;
       public       postgres    false    218    224    2839            ,           2606    220756    cultivates fk19f67e908ccde5cb    FK CONSTRAINT     }   ALTER TABLE ONLY public.cultivates
    ADD CONSTRAINT fk19f67e908ccde5cb FOREIGN KEY (id) REFERENCES public.timeperiods(id);
 G   ALTER TABLE ONLY public.cultivates DROP CONSTRAINT fk19f67e908ccde5cb;
       public       postgres    false    2847    199    228            5           2606    220761    implants fk221528268ccde5cb    FK CONSTRAINT     {   ALTER TABLE ONLY public.implants
    ADD CONSTRAINT fk221528268ccde5cb FOREIGN KEY (id) REFERENCES public.timeperiods(id);
 E   ALTER TABLE ONLY public.implants DROP CONSTRAINT fk221528268ccde5cb;
       public       postgres    false    204    2847    228            )           2606    220766    blooms fk26d7eae8ccde5cb    FK CONSTRAINT     x   ALTER TABLE ONLY public.blooms
    ADD CONSTRAINT fk26d7eae8ccde5cb FOREIGN KEY (id) REFERENCES public.timeperiods(id);
 B   ALTER TABLE ONLY public.blooms DROP CONSTRAINT fk26d7eae8ccde5cb;
       public       postgres    false    198    2847    228            q           2606    220771    taxa fk28000297913f13    FK CONSTRAINT     o   ALTER TABLE ONLY public.taxa
    ADD CONSTRAINT fk28000297913f13 FOREIGN KEY (id) REFERENCES public.nodes(id);
 ?   ALTER TABLE ONLY public.taxa DROP CONSTRAINT fk28000297913f13;
       public       postgres    false    2821    212    227            -           2606    221029    cultivates fk29485d042eabff88    FK CONSTRAINT     }   ALTER TABLE ONLY public.cultivates
    ADD CONSTRAINT fk29485d042eabff88 FOREIGN KEY (id) REFERENCES public.timeperiods(id);
 G   ALTER TABLE ONLY public.cultivates DROP CONSTRAINT fk29485d042eabff88;
       public       postgres    false    228    199    2847            O           2606    220914    nodes fk2f6a097b1d01c01b    FK CONSTRAINT     u   ALTER TABLE ONLY public.nodes
    ADD CONSTRAINT fk2f6a097b1d01c01b FOREIGN KEY (id) REFERENCES public.subjects(id);
 B   ALTER TABLE ONLY public.nodes DROP CONSTRAINT fk2f6a097b1d01c01b;
       public       postgres    false    212    225    2841            P           2606    220919    nodes fk2f6a097ba7a1ab5c    FK CONSTRAINT     v   ALTER TABLE ONLY public.nodes
    ADD CONSTRAINT fk2f6a097ba7a1ab5c FOREIGN KEY (parent) REFERENCES public.nodes(id);
 B   ALTER TABLE ONLY public.nodes DROP CONSTRAINT fk2f6a097ba7a1ab5c;
       public       postgres    false    2821    212    212            #           2606    220954 )   aftercultures_mappping fk3741c64f12572275    FK CONSTRAINT     �   ALTER TABLE ONLY public.aftercultures_mappping
    ADD CONSTRAINT fk3741c64f12572275 FOREIGN KEY (id) REFERENCES public.plants(id);
 S   ALTER TABLE ONLY public.aftercultures_mappping DROP CONSTRAINT fk3741c64f12572275;
       public       postgres    false    196    2829    218            "           2606    220949 )   aftercultures_mappping fk3741c64f9aa621a5    FK CONSTRAINT     �   ALTER TABLE ONLY public.aftercultures_mappping
    ADD CONSTRAINT fk3741c64f9aa621a5 FOREIGN KEY (aftercultures_id) REFERENCES public.plants(id);
 S   ALTER TABLE ONLY public.aftercultures_mappping DROP CONSTRAINT fk3741c64f9aa621a5;
       public       postgres    false    2829    218    196            v           2606    220999    timeperiods fk3c93bca545210cfe    FK CONSTRAINT     �   ALTER TABLE ONLY public.timeperiods
    ADD CONSTRAINT fk3c93bca545210cfe FOREIGN KEY (nextid) REFERENCES public.timeperiods(id);
 H   ALTER TABLE ONLY public.timeperiods DROP CONSTRAINT fk3c93bca545210cfe;
       public       postgres    false    228    228    2847            w           2606    221004    timeperiods fk3c93bca5b7d403df    FK CONSTRAINT     �   ALTER TABLE ONLY public.timeperiods
    ADD CONSTRAINT fk3c93bca5b7d403df FOREIGN KEY (subjectref) REFERENCES public.subjects(id);
 H   ALTER TABLE ONLY public.timeperiods DROP CONSTRAINT fk3c93bca5b7d403df;
       public       postgres    false    228    225    2841            *           2606    221024    blooms fk46d4c1fe2eabff88    FK CONSTRAINT     y   ALTER TABLE ONLY public.blooms
    ADD CONSTRAINT fk46d4c1fe2eabff88 FOREIGN KEY (id) REFERENCES public.timeperiods(id);
 C   ALTER TABLE ONLY public.blooms DROP CONSTRAINT fk46d4c1fe2eabff88;
       public       postgres    false    198    2847    228            ?           2606    220994    interactions fk4deb4107393c1ce7    FK CONSTRAINT     �   ALTER TABLE ONLY public.interactions
    ADD CONSTRAINT fk4deb4107393c1ce7 FOREIGN KEY (impactsubject) REFERENCES public.subjects(id);
 I   ALTER TABLE ONLY public.interactions DROP CONSTRAINT fk4deb4107393c1ce7;
       public       postgres    false    205    225    2841            >           2606    220989    interactions fk4deb41074ef0062    FK CONSTRAINT        ALTER TABLE ONLY public.interactions
    ADD CONSTRAINT fk4deb41074ef0062 FOREIGN KEY (object) REFERENCES public.subjects(id);
 H   ALTER TABLE ONLY public.interactions DROP CONSTRAINT fk4deb41074ef0062;
       public       postgres    false    205    225    2841            <           2606    220979    interactions fk4deb41079a621a66    FK CONSTRAINT     �   ALTER TABLE ONLY public.interactions
    ADD CONSTRAINT fk4deb41079a621a66 FOREIGN KEY (subject) REFERENCES public.subjects(id);
 I   ALTER TABLE ONLY public.interactions DROP CONSTRAINT fk4deb41079a621a66;
       public       postgres    false    205    2841    225            =           2606    220984    interactions fk4deb4107b43a11a4    FK CONSTRAINT     �   ALTER TABLE ONLY public.interactions
    ADD CONSTRAINT fk4deb4107b43a11a4 FOREIGN KEY (predicate) REFERENCES public.predicates(id);
 I   ALTER TABLE ONLY public.interactions DROP CONSTRAINT fk4deb4107b43a11a4;
       public       postgres    false    205    2833    220            D           2606    220776    lifetimes fk5f1450de8ccde5cb    FK CONSTRAINT     |   ALTER TABLE ONLY public.lifetimes
    ADD CONSTRAINT fk5f1450de8ccde5cb FOREIGN KEY (id) REFERENCES public.timeperiods(id);
 F   ALTER TABLE ONLY public.lifetimes DROP CONSTRAINT fk5f1450de8ccde5cb;
       public       postgres    false    207    2847    228            M           2606    220781    nodes fk5f7a15475d40c90    FK CONSTRAINT     u   ALTER TABLE ONLY public.nodes
    ADD CONSTRAINT fk5f7a15475d40c90 FOREIGN KEY (parent) REFERENCES public.nodes(id);
 A   ALTER TABLE ONLY public.nodes DROP CONSTRAINT fk5f7a15475d40c90;
       public       postgres    false    212    2821    212            N           2606    220786    nodes fk5f7a1547a73b57d7    FK CONSTRAINT     u   ALTER TABLE ONLY public.nodes
    ADD CONSTRAINT fk5f7a1547a73b57d7 FOREIGN KEY (id) REFERENCES public.subjects(id);
 B   ALTER TABLE ONLY public.nodes DROP CONSTRAINT fk5f7a1547a73b57d7;
       public       postgres    false    2841    212    225            V           2606    220791    placements fk66ed67c2aa76088d    FK CONSTRAINT     ~   ALTER TABLE ONLY public.placements
    ADD CONSTRAINT fk66ed67c2aa76088d FOREIGN KEY (plantref) REFERENCES public.plants(id);
 G   ALTER TABLE ONLY public.placements DROP CONSTRAINT fk66ed67c2aa76088d;
       public       postgres    false    218    2829    217            W           2606    220796    placements fk66ed67c2c82714ab    FK CONSTRAINT        ALTER TABLE ONLY public.placements
    ADD CONSTRAINT fk66ed67c2c82714ab FOREIGN KEY (id) REFERENCES public.patchelements(id);
 G   ALTER TABLE ONLY public.placements DROP CONSTRAINT fk66ed67c2c82714ab;
       public       postgres    false    217    213    2823            &           2606    220801    animals fk6c559d11f6e3815    FK CONSTRAINT     u   ALTER TABLE ONLY public.animals
    ADD CONSTRAINT fk6c559d11f6e3815 FOREIGN KEY (id) REFERENCES public.species(id);
 C   ALTER TABLE ONLY public.animals DROP CONSTRAINT fk6c559d11f6e3815;
       public       postgres    false    197    224    2839            l           2606    221009    sowings fk75dccd6d2eabff88    FK CONSTRAINT     z   ALTER TABLE ONLY public.sowings
    ADD CONSTRAINT fk75dccd6d2eabff88 FOREIGN KEY (id) REFERENCES public.timeperiods(id);
 D   ALTER TABLE ONLY public.sowings DROP CONSTRAINT fk75dccd6d2eabff88;
       public       postgres    false    223    2847    228            G           2606    220806    medias fk79a375cfd1169c84    FK CONSTRAINT     {   ALTER TABLE ONLY public.medias
    ADD CONSTRAINT fk79a375cfd1169c84 FOREIGN KEY (subject) REFERENCES public.subjects(id);
 C   ALTER TABLE ONLY public.medias DROP CONSTRAINT fk79a375cfd1169c84;
       public       postgres    false    208    225    2841            t           2606    220811    timeperiods fk7cf2f94719c1cd50    FK CONSTRAINT     �   ALTER TABLE ONLY public.timeperiods
    ADD CONSTRAINT fk7cf2f94719c1cd50 FOREIGN KEY (nextid) REFERENCES public.timeperiods(id);
 H   ALTER TABLE ONLY public.timeperiods DROP CONSTRAINT fk7cf2f94719c1cd50;
       public       postgres    false    2847    228    228            u           2606    220816    timeperiods fk7cf2f94735c987c7    FK CONSTRAINT     �   ALTER TABLE ONLY public.timeperiods
    ADD CONSTRAINT fk7cf2f94735c987c7 FOREIGN KEY (subjectref) REFERENCES public.subjects(id);
 H   ALTER TABLE ONLY public.timeperiods DROP CONSTRAINT fk7cf2f94735c987c7;
       public       postgres    false    228    225    2841            S           2606    220821     patchelements fk8173b2635bd013bf    FK CONSTRAINT     �   ALTER TABLE ONLY public.patchelements
    ADD CONSTRAINT fk8173b2635bd013bf FOREIGN KEY (patchref) REFERENCES public.patches(id);
 J   ALTER TABLE ONLY public.patchelements DROP CONSTRAINT fk8173b2635bd013bf;
       public       postgres    false    215    213    2825            T           2606    221044     patchelements fk82677ae7249fb749    FK CONSTRAINT     �   ALTER TABLE ONLY public.patchelements
    ADD CONSTRAINT fk82677ae7249fb749 FOREIGN KEY (patchref) REFERENCES public.patches(id);
 J   ALTER TABLE ONLY public.patchelements DROP CONSTRAINT fk82677ae7249fb749;
       public       postgres    false    215    213    2825            n           2606    220826    species fk8c96c8897913f13    FK CONSTRAINT     s   ALTER TABLE ONLY public.species
    ADD CONSTRAINT fk8c96c8897913f13 FOREIGN KEY (id) REFERENCES public.nodes(id);
 C   ALTER TABLE ONLY public.species DROP CONSTRAINT fk8c96c8897913f13;
       public       postgres    false    224    2821    212            i           2606    221019     seedmaturitys fk9257bb5f2eabff88    FK CONSTRAINT     �   ALTER TABLE ONLY public.seedmaturitys
    ADD CONSTRAINT fk9257bb5f2eabff88 FOREIGN KEY (id) REFERENCES public.timeperiods(id);
 J   ALTER TABLE ONLY public.seedmaturitys DROP CONSTRAINT fk9257bb5f2eabff88;
       public       postgres    false    2847    222    228            8           2606    220831    interactions fk9468ddb1131aec42    FK CONSTRAINT     �   ALTER TABLE ONLY public.interactions
    ADD CONSTRAINT fk9468ddb1131aec42 FOREIGN KEY (impactsubject) REFERENCES public.subjects(id);
 I   ALTER TABLE ONLY public.interactions DROP CONSTRAINT fk9468ddb1131aec42;
       public       postgres    false    225    205    2841            9           2606    220836    interactions fk9468ddb1492ba278    FK CONSTRAINT     �   ALTER TABLE ONLY public.interactions
    ADD CONSTRAINT fk9468ddb1492ba278 FOREIGN KEY (predicate) REFERENCES public.predicates(id);
 I   ALTER TABLE ONLY public.interactions DROP CONSTRAINT fk9468ddb1492ba278;
       public       postgres    false    205    220    2833            :           2606    220841    interactions fk9468ddb1cc64fcb7    FK CONSTRAINT     �   ALTER TABLE ONLY public.interactions
    ADD CONSTRAINT fk9468ddb1cc64fcb7 FOREIGN KEY (object) REFERENCES public.subjects(id);
 I   ALTER TABLE ONLY public.interactions DROP CONSTRAINT fk9468ddb1cc64fcb7;
       public       postgres    false    225    2841    205            ;           2606    220846    interactions fk9468ddb1d1169c84    FK CONSTRAINT     �   ALTER TABLE ONLY public.interactions
    ADD CONSTRAINT fk9468ddb1d1169c84 FOREIGN KEY (subject) REFERENCES public.subjects(id);
 I   ALTER TABLE ONLY public.interactions DROP CONSTRAINT fk9468ddb1d1169c84;
       public       postgres    false    205    2841    225            J           2606    220851    names fk95f4359ad2c6a1f8    FK CONSTRAINT     t   ALTER TABLE ONLY public.names
    ADD CONSTRAINT fk95f4359ad2c6a1f8 FOREIGN KEY (node) REFERENCES public.nodes(id);
 B   ALTER TABLE ONLY public.names DROP CONSTRAINT fk95f4359ad2c6a1f8;
       public       postgres    false    212    2821    210            k           2606    220856    sowings fk9c12d1d08ccde5cb    FK CONSTRAINT     z   ALTER TABLE ONLY public.sowings
    ADD CONSTRAINT fk9c12d1d08ccde5cb FOREIGN KEY (id) REFERENCES public.timeperiods(id);
 D   ALTER TABLE ONLY public.sowings DROP CONSTRAINT fk9c12d1d08ccde5cb;
       public       postgres    false    228    2847    223            R           2606    221071    nodes fk_1799fca2    FK CONSTRAINT     o   ALTER TABLE ONLY public.nodes
    ADD CONSTRAINT fk_1799fca2 FOREIGN KEY (parent) REFERENCES public.nodes(id);
 ;   ALTER TABLE ONLY public.nodes DROP CONSTRAINT fk_1799fca2;
       public       postgres    false    212    2821    212            I           2606    221061    medias fk_194d2dde    FK CONSTRAINT     t   ALTER TABLE ONLY public.medias
    ADD CONSTRAINT fk_194d2dde FOREIGN KEY (subject) REFERENCES public.subjects(id);
 <   ALTER TABLE ONLY public.medias DROP CONSTRAINT fk_194d2dde;
       public       postgres    false    225    208    2841            L           2606    221126    names fk_2818358a    FK CONSTRAINT     m   ALTER TABLE ONLY public.names
    ADD CONSTRAINT fk_2818358a FOREIGN KEY (node) REFERENCES public.nodes(id);
 ;   ALTER TABLE ONLY public.names DROP CONSTRAINT fk_2818358a;
       public       postgres    false    210    2821    212            $           2606    221101 "   aftercultures_mappping fk_35f66856    FK CONSTRAINT     �   ALTER TABLE ONLY public.aftercultures_mappping
    ADD CONSTRAINT fk_35f66856 FOREIGN KEY (aftercultures_id) REFERENCES public.plants(id);
 L   ALTER TABLE ONLY public.aftercultures_mappping DROP CONSTRAINT fk_35f66856;
       public       postgres    false    196    2829    218            [           2606    221206    placements fk_42f126f5    FK CONSTRAINT     w   ALTER TABLE ONLY public.placements
    ADD CONSTRAINT fk_42f126f5 FOREIGN KEY (plantref) REFERENCES public.plants(id);
 @   ALTER TABLE ONLY public.placements DROP CONSTRAINT fk_42f126f5;
       public       postgres    false    217    218    2829            y           2606    221156    timeperiods fk_43a3d0a4    FK CONSTRAINT     |   ALTER TABLE ONLY public.timeperiods
    ADD CONSTRAINT fk_43a3d0a4 FOREIGN KEY (subjectref) REFERENCES public.subjects(id);
 A   ALTER TABLE ONLY public.timeperiods DROP CONSTRAINT fk_43a3d0a4;
       public       postgres    false    225    228    2841            A           2606    221136    interactions fk_43e8ec37    FK CONSTRAINT     ~   ALTER TABLE ONLY public.interactions
    ADD CONSTRAINT fk_43e8ec37 FOREIGN KEY (predicate) REFERENCES public.predicates(id);
 B   ALTER TABLE ONLY public.interactions DROP CONSTRAINT fk_43e8ec37;
       public       postgres    false    205    220    2833            @           2606    221131    interactions fk_4d48dc2f    FK CONSTRAINT     z   ALTER TABLE ONLY public.interactions
    ADD CONSTRAINT fk_4d48dc2f FOREIGN KEY (subject) REFERENCES public.subjects(id);
 B   ALTER TABLE ONLY public.interactions DROP CONSTRAINT fk_4d48dc2f;
       public       postgres    false    225    2841    205            ^           2606    221086    plants fk_4fc23e80    FK CONSTRAINT     n   ALTER TABLE ONLY public.plants
    ADD CONSTRAINT fk_4fc23e80 FOREIGN KEY (id) REFERENCES public.species(id);
 <   ALTER TABLE ONLY public.plants DROP CONSTRAINT fk_4fc23e80;
       public       postgres    false    2839    224    218            U           2606    221196    patchelements fk_52810082    FK CONSTRAINT     {   ALTER TABLE ONLY public.patchelements
    ADD CONSTRAINT fk_52810082 FOREIGN KEY (patchref) REFERENCES public.patches(id);
 C   ALTER TABLE ONLY public.patchelements DROP CONSTRAINT fk_52810082;
       public       postgres    false    2825    213    215            j           2606    221171    seedmaturitys fk_551a11ff    FK CONSTRAINT     y   ALTER TABLE ONLY public.seedmaturitys
    ADD CONSTRAINT fk_551a11ff FOREIGN KEY (id) REFERENCES public.timeperiods(id);
 C   ALTER TABLE ONLY public.seedmaturitys DROP CONSTRAINT fk_551a11ff;
       public       postgres    false    228    2847    222            Z           2606    221201    placements fk_58a4e232    FK CONSTRAINT     x   ALTER TABLE ONLY public.placements
    ADD CONSTRAINT fk_58a4e232 FOREIGN KEY (id) REFERENCES public.patchelements(id);
 @   ALTER TABLE ONLY public.placements DROP CONSTRAINT fk_58a4e232;
       public       postgres    false    217    2823    213            .           2606    221181    cultivates fk_6db50684    FK CONSTRAINT     v   ALTER TABLE ONLY public.cultivates
    ADD CONSTRAINT fk_6db50684 FOREIGN KEY (id) REFERENCES public.timeperiods(id);
 @   ALTER TABLE ONLY public.cultivates DROP CONSTRAINT fk_6db50684;
       public       postgres    false    228    199    2847            p           2606    221081    species fk_6df987d9    FK CONSTRAINT     m   ALTER TABLE ONLY public.species
    ADD CONSTRAINT fk_6df987d9 FOREIGN KEY (id) REFERENCES public.nodes(id);
 =   ALTER TABLE ONLY public.species DROP CONSTRAINT fk_6df987d9;
       public       postgres    false    224    212    2821            (           2606    221111    animals fk_84bf138b    FK CONSTRAINT     o   ALTER TABLE ONLY public.animals
    ADD CONSTRAINT fk_84bf138b FOREIGN KEY (id) REFERENCES public.species(id);
 =   ALTER TABLE ONLY public.animals DROP CONSTRAINT fk_84bf138b;
       public       postgres    false    2839    197    224            s           2606    221116    taxa fk_977b2c4b    FK CONSTRAINT     j   ALTER TABLE ONLY public.taxa
    ADD CONSTRAINT fk_977b2c4b FOREIGN KEY (id) REFERENCES public.nodes(id);
 :   ALTER TABLE ONLY public.taxa DROP CONSTRAINT fk_977b2c4b;
       public       postgres    false    212    2821    227            4           2606    221166    harvests fk_9a9122eb    FK CONSTRAINT     t   ALTER TABLE ONLY public.harvests
    ADD CONSTRAINT fk_9a9122eb FOREIGN KEY (id) REFERENCES public.timeperiods(id);
 >   ALTER TABLE ONLY public.harvests DROP CONSTRAINT fk_9a9122eb;
       public       postgres    false    203    228    2847            1           2606    221076    effects fk_b1b33f7a    FK CONSTRAINT     m   ALTER TABLE ONLY public.effects
    ADD CONSTRAINT fk_b1b33f7a FOREIGN KEY (id) REFERENCES public.nodes(id);
 =   ALTER TABLE ONLY public.effects DROP CONSTRAINT fk_b1b33f7a;
       public       postgres    false    2821    202    212            c           2606    221091     precultures_mappping fk_b2fb0a2b    FK CONSTRAINT     �   ALTER TABLE ONLY public.precultures_mappping
    ADD CONSTRAINT fk_b2fb0a2b FOREIGN KEY (precultures_id) REFERENCES public.plants(id);
 J   ALTER TABLE ONLY public.precultures_mappping DROP CONSTRAINT fk_b2fb0a2b;
       public       postgres    false    219    2829    218            g           2606    221121    predicates fk_c7ecb262    FK CONSTRAINT     |   ALTER TABLE ONLY public.predicates
    ADD CONSTRAINT fk_c7ecb262 FOREIGN KEY (parentref) REFERENCES public.predicates(id);
 @   ALTER TABLE ONLY public.predicates DROP CONSTRAINT fk_c7ecb262;
       public       postgres    false    220    220    2833            +           2606    221176    blooms fk_cffa174f    FK CONSTRAINT     r   ALTER TABLE ONLY public.blooms
    ADD CONSTRAINT fk_cffa174f FOREIGN KEY (id) REFERENCES public.timeperiods(id);
 <   ALTER TABLE ONLY public.blooms DROP CONSTRAINT fk_cffa174f;
       public       postgres    false    198    228    2847            %           2606    221106 "   aftercultures_mappping fk_d5beeecf    FK CONSTRAINT     }   ALTER TABLE ONLY public.aftercultures_mappping
    ADD CONSTRAINT fk_d5beeecf FOREIGN KEY (id) REFERENCES public.plants(id);
 L   ALTER TABLE ONLY public.aftercultures_mappping DROP CONSTRAINT fk_d5beeecf;
       public       postgres    false    2829    196    218            F           2606    221191    lifetimes fk_e0aa9f58    FK CONSTRAINT     u   ALTER TABLE ONLY public.lifetimes
    ADD CONSTRAINT fk_e0aa9f58 FOREIGN KEY (id) REFERENCES public.timeperiods(id);
 ?   ALTER TABLE ONLY public.lifetimes DROP CONSTRAINT fk_e0aa9f58;
       public       postgres    false    207    2847    228            B           2606    221141    interactions fk_e0ba4134    FK CONSTRAINT     y   ALTER TABLE ONLY public.interactions
    ADD CONSTRAINT fk_e0ba4134 FOREIGN KEY (object) REFERENCES public.subjects(id);
 B   ALTER TABLE ONLY public.interactions DROP CONSTRAINT fk_e0ba4134;
       public       postgres    false    225    2841    205            d           2606    221096     precultures_mappping fk_e492aee8    FK CONSTRAINT     {   ALTER TABLE ONLY public.precultures_mappping
    ADD CONSTRAINT fk_e492aee8 FOREIGN KEY (id) REFERENCES public.plants(id);
 J   ALTER TABLE ONLY public.precultures_mappping DROP CONSTRAINT fk_e492aee8;
       public       postgres    false    218    219    2829            m           2606    221161    sowings fk_e9af2ae    FK CONSTRAINT     r   ALTER TABLE ONLY public.sowings
    ADD CONSTRAINT fk_e9af2ae FOREIGN KEY (id) REFERENCES public.timeperiods(id);
 <   ALTER TABLE ONLY public.sowings DROP CONSTRAINT fk_e9af2ae;
       public       postgres    false    2847    228    223            x           2606    221151    timeperiods fk_f16d7073    FK CONSTRAINT     {   ALTER TABLE ONLY public.timeperiods
    ADD CONSTRAINT fk_f16d7073 FOREIGN KEY (nextid) REFERENCES public.timeperiods(id);
 A   ALTER TABLE ONLY public.timeperiods DROP CONSTRAINT fk_f16d7073;
       public       postgres    false    228    2847    228            C           2606    221146    interactions fk_f34fd50e    FK CONSTRAINT     �   ALTER TABLE ONLY public.interactions
    ADD CONSTRAINT fk_f34fd50e FOREIGN KEY (impactsubject) REFERENCES public.subjects(id);
 B   ALTER TABLE ONLY public.interactions DROP CONSTRAINT fk_f34fd50e;
       public       postgres    false    205    2841    225            Q           2606    221066    nodes fk_f44eadb2    FK CONSTRAINT     n   ALTER TABLE ONLY public.nodes
    ADD CONSTRAINT fk_f44eadb2 FOREIGN KEY (id) REFERENCES public.subjects(id);
 ;   ALTER TABLE ONLY public.nodes DROP CONSTRAINT fk_f44eadb2;
       public       postgres    false    2841    212    225            7           2606    221186    implants fk_fcf6ea49    FK CONSTRAINT     t   ALTER TABLE ONLY public.implants
    ADD CONSTRAINT fk_fcf6ea49 FOREIGN KEY (id) REFERENCES public.timeperiods(id);
 >   ALTER TABLE ONLY public.implants DROP CONSTRAINT fk_fcf6ea49;
       public       postgres    false    204    2847    228            3           2606    221014    harvests fka32a84db2eabff88    FK CONSTRAINT     {   ALTER TABLE ONLY public.harvests
    ADD CONSTRAINT fka32a84db2eabff88 FOREIGN KEY (id) REFERENCES public.timeperiods(id);
 E   ALTER TABLE ONLY public.harvests DROP CONSTRAINT fka32a84db2eabff88;
       public       postgres    false    203    2847    228            '           2606    220959    animals fka750bc7039da9f19    FK CONSTRAINT     v   ALTER TABLE ONLY public.animals
    ADD CONSTRAINT fka750bc7039da9f19 FOREIGN KEY (id) REFERENCES public.species(id);
 D   ALTER TABLE ONLY public.animals DROP CONSTRAINT fka750bc7039da9f19;
       public       postgres    false    2839    224    197            H           2606    220909    medias fkb7b10a309a621a66    FK CONSTRAINT     {   ALTER TABLE ONLY public.medias
    ADD CONSTRAINT fkb7b10a309a621a66 FOREIGN KEY (subject) REFERENCES public.subjects(id);
 C   ALTER TABLE ONLY public.medias DROP CONSTRAINT fkb7b10a309a621a66;
       public       postgres    false    2841    225    208            K           2606    220974    names fkbc93d93e6e5f3328    FK CONSTRAINT     t   ALTER TABLE ONLY public.names
    ADD CONSTRAINT fkbc93d93e6e5f3328 FOREIGN KEY (node) REFERENCES public.nodes(id);
 B   ALTER TABLE ONLY public.names DROP CONSTRAINT fkbc93d93e6e5f3328;
       public       postgres    false    210    212    2821            Y           2606    221054    placements fkc0e10c9e3248e604    FK CONSTRAINT     ~   ALTER TABLE ONLY public.placements
    ADD CONSTRAINT fkc0e10c9e3248e604 FOREIGN KEY (plantref) REFERENCES public.plants(id);
 G   ALTER TABLE ONLY public.placements DROP CONSTRAINT fkc0e10c9e3248e604;
       public       postgres    false    217    218    2829            X           2606    221049    placements fkc0e10c9eb90128e9    FK CONSTRAINT        ALTER TABLE ONLY public.placements
    ADD CONSTRAINT fkc0e10c9eb90128e9 FOREIGN KEY (id) REFERENCES public.patchelements(id);
 G   ALTER TABLE ONLY public.placements DROP CONSTRAINT fkc0e10c9eb90128e9;
       public       postgres    false    2823    217    213            6           2606    221034    implants fkc5aecc622eabff88    FK CONSTRAINT     {   ALTER TABLE ONLY public.implants
    ADD CONSTRAINT fkc5aecc622eabff88 FOREIGN KEY (id) REFERENCES public.timeperiods(id);
 E   ALTER TABLE ONLY public.implants DROP CONSTRAINT fkc5aecc622eabff88;
       public       postgres    false    2847    228    204            o           2606    220929    species fkc60024dad5eac857    FK CONSTRAINT     t   ALTER TABLE ONLY public.species
    ADD CONSTRAINT fkc60024dad5eac857 FOREIGN KEY (id) REFERENCES public.nodes(id);
 D   ALTER TABLE ONLY public.species DROP CONSTRAINT fkc60024dad5eac857;
       public       postgres    false    212    224    2821                        2606    220861 )   aftercultures_mappping fkca638c5e54978f1c    FK CONSTRAINT     �   ALTER TABLE ONLY public.aftercultures_mappping
    ADD CONSTRAINT fkca638c5e54978f1c FOREIGN KEY (aftercultures_id) REFERENCES public.plants(id);
 S   ALTER TABLE ONLY public.aftercultures_mappping DROP CONSTRAINT fkca638c5e54978f1c;
       public       postgres    false    196    218    2829            !           2606    220866 )   aftercultures_mappping fkca638c5e88598ab8    FK CONSTRAINT     �   ALTER TABLE ONLY public.aftercultures_mappping
    ADD CONSTRAINT fkca638c5e88598ab8 FOREIGN KEY (id) REFERENCES public.plants(id);
 S   ALTER TABLE ONLY public.aftercultures_mappping DROP CONSTRAINT fkca638c5e88598ab8;
       public       postgres    false    196    218    2829            r           2606    220964    taxa fkd9948beed5eac857    FK CONSTRAINT     q   ALTER TABLE ONLY public.taxa
    ADD CONSTRAINT fkd9948beed5eac857 FOREIGN KEY (id) REFERENCES public.nodes(id);
 A   ALTER TABLE ONLY public.taxa DROP CONSTRAINT fkd9948beed5eac857;
       public       postgres    false    227    212    2821            E           2606    221039    lifetimes fkd9f61e132eabff88    FK CONSTRAINT     |   ALTER TABLE ONLY public.lifetimes
    ADD CONSTRAINT fkd9f61e132eabff88 FOREIGN KEY (id) REFERENCES public.timeperiods(id);
 F   ALTER TABLE ONLY public.lifetimes DROP CONSTRAINT fkd9f61e132eabff88;
       public       postgres    false    228    207    2847            _           2606    220871 '   precultures_mappping fkdc68415b165d28d1    FK CONSTRAINT     �   ALTER TABLE ONLY public.precultures_mappping
    ADD CONSTRAINT fkdc68415b165d28d1 FOREIGN KEY (precultures_id) REFERENCES public.plants(id);
 Q   ALTER TABLE ONLY public.precultures_mappping DROP CONSTRAINT fkdc68415b165d28d1;
       public       postgres    false    219    2829    218            `           2606    220876 '   precultures_mappping fkdc68415b88598ab8    FK CONSTRAINT     �   ALTER TABLE ONLY public.precultures_mappping
    ADD CONSTRAINT fkdc68415b88598ab8 FOREIGN KEY (id) REFERENCES public.plants(id);
 Q   ALTER TABLE ONLY public.precultures_mappping DROP CONSTRAINT fkdc68415b88598ab8;
       public       postgres    false    219    2829    218            b           2606    220944 '   precultures_mappping fke5e2ed0c12572275    FK CONSTRAINT     �   ALTER TABLE ONLY public.precultures_mappping
    ADD CONSTRAINT fke5e2ed0c12572275 FOREIGN KEY (id) REFERENCES public.plants(id);
 Q   ALTER TABLE ONLY public.precultures_mappping DROP CONSTRAINT fke5e2ed0c12572275;
       public       postgres    false    218    219    2829            a           2606    220939 '   precultures_mappping fke5e2ed0c82c3648c    FK CONSTRAINT     �   ALTER TABLE ONLY public.precultures_mappping
    ADD CONSTRAINT fke5e2ed0c82c3648c FOREIGN KEY (precultures_id) REFERENCES public.plants(id);
 Q   ALTER TABLE ONLY public.precultures_mappping DROP CONSTRAINT fke5e2ed0c82c3648c;
       public       postgres    false    219    2829    218            f           2606    220969    predicates fkeab859b44f779e03    FK CONSTRAINT     �   ALTER TABLE ONLY public.predicates
    ADD CONSTRAINT fkeab859b44f779e03 FOREIGN KEY (parentref) REFERENCES public.predicates(id);
 G   ALTER TABLE ONLY public.predicates DROP CONSTRAINT fkeab859b44f779e03;
       public       postgres    false    220    220    2833            2           2606    220881    harvests fkeae714e88ccde5cb    FK CONSTRAINT     {   ALTER TABLE ONLY public.harvests
    ADD CONSTRAINT fkeae714e88ccde5cb FOREIGN KEY (id) REFERENCES public.timeperiods(id);
 E   ALTER TABLE ONLY public.harvests DROP CONSTRAINT fkeae714e88ccde5cb;
       public       postgres    false    2847    228    203            /           2606    220886    effects fked1facb097913f13    FK CONSTRAINT     t   ALTER TABLE ONLY public.effects
    ADD CONSTRAINT fked1facb097913f13 FOREIGN KEY (id) REFERENCES public.nodes(id);
 D   ALTER TABLE ONLY public.effects DROP CONSTRAINT fked1facb097913f13;
       public       postgres    false    212    2821    202            e           2606    220891    predicates fkee168f909a170b64    FK CONSTRAINT     �   ALTER TABLE ONLY public.predicates
    ADD CONSTRAINT fkee168f909a170b64 FOREIGN KEY (parentref) REFERENCES public.predicates(id);
 G   ALTER TABLE ONLY public.predicates DROP CONSTRAINT fkee168f909a170b64;
       public       postgres    false    220    2833    220            0           2606    220924    effects fkf07cc02fd5eac857    FK CONSTRAINT     t   ALTER TABLE ONLY public.effects
    ADD CONSTRAINT fkf07cc02fd5eac857 FOREIGN KEY (id) REFERENCES public.nodes(id);
 D   ALTER TABLE ONLY public.effects DROP CONSTRAINT fkf07cc02fd5eac857;
       public       postgres    false    202    212    2821            \           2606    220896    plants fkfa03d1e4f6e3815    FK CONSTRAINT     t   ALTER TABLE ONLY public.plants
    ADD CONSTRAINT fkfa03d1e4f6e3815 FOREIGN KEY (id) REFERENCES public.species(id);
 B   ALTER TABLE ONLY public.plants DROP CONSTRAINT fkfa03d1e4f6e3815;
       public       postgres    false    218    2839    224            h           2606    220901     seedmaturitys fkfde48b5f8ccde5cb    FK CONSTRAINT     �   ALTER TABLE ONLY public.seedmaturitys
    ADD CONSTRAINT fkfde48b5f8ccde5cb FOREIGN KEY (id) REFERENCES public.timeperiods(id);
 J   ALTER TABLE ONLY public.seedmaturitys DROP CONSTRAINT fkfde48b5f8ccde5cb;
       public       postgres    false    222    2847    228            �   8   x�%��  �7�Ђ�0�?�F^�e�ˤ�� =������jU	e�?Ug ����      �   #   x�327�227b ��26b3c 6����� _J      �      x������ � �      �   )   x�34�4�4�24�P�`�� BC(3e	����T� ��      �   u  x�5�[r�0���I�o�.��9�=P����L� ��v��������{^��u-����>���aL�������0�z4L��LwL��x��5߂9׫y�}/�����#l���vV��>x5f:�7[˲��\=�X?�^�]�q�	��rlw8v\=��k�wV�ﺶ�
������o����&�8n"r~3��.]�n��ߎ}�%h/v������^��U��k�Э]�̸}��3p�o�9,���o�¶����q�Ƣݿ����Qp\Ix�U�^;p���?���=��#��3@W��� q9�s뿓XO���z�g?ģ�Z����� A}����ߋ�O�]��J���ӳ�Qvv
�,
���l�<O��P�-������xp��6t<�Г�K�?C�	���a�ZM1��@��,b���?E�c� /�l�XJk�|%iBuR�Er/��3���S��N�p�!6����,�TG�4/�-�1��m������u�9&�7i�t�E��� ��
��k;A��|L�n{Mq	}��!��S�~�B�Ss��&�
�S��5˂t.T"�z�����;Z ZajD��0qBӓ
-�%�+0����iߤ�
��J��=����b�"�=uC�S�{Ev�[äG��b�i�!h=�I�IV���u��䀌��.�v�/�>z��L͇z�U��
�����|�08�L��b���&|�ɎEϷ}��iY�I���*�]�����]�R�w�91S'�]�Iވ�v>l3��4A��L���~p���V�cCO)Z�S�A��'HB�ꁬ�O��ރ�C�C%�z�=�]�D�����&�,L���T�Y�.T�χ�/r&�Vg���0�uU��� �1�B�('��w�G��)�}Nh�X���.dKn������ˠ��a�>�CXk	��29堉����z s�)4�{�v�G�A�}�I�0�k�\p� �+ �T��v2<�R����'��A%I@�^va���\�ꈃ*�.�����mm����[nv�Ht��W�*���*�F~N���5K�j���"�=ɦ�[9�p�s�$ʍhٛW�U��G��d_�S���w����[�B{�n�֭p؏#V/'��$�����]����Hy�����G�o b�ӕ��8����VH$(�Ak�[nv�(��Խ�R�vnW*tےN�9���z��S�7���[i�n�(�R��dz]�ĂO�G�-�)LAf�ҭ���T[�[��L6��V�Yj�~��E�-Id���)��=G¦�,�ڔJ�n�XYʽ�,�[E��J8��(�r)H,�O���?Л[H�ڒABnĕ��� k}Hb��G�={�_�)�&����*�(�������#�      �      x�3�0������ �M      �   +   x��A�0�{���[:��� ���#%#���գ/�z|?}+�      �   %   x�34�24�24�2��22�22�2��26����� A(      �   �  x�e�۹� ���w3wD���������� KI��W��������߿���g���-������w����{�~�{�~��p���p����� d<�� {����_.��/�Q�s	Iߗ�{�r�Gj����̿<�C�h�C�x�GR�_6{�qy��$;�����1χ�1���3f{Ȟ_[�cn�]{��0'{\e�d�VqR�׽��.�=�U��a}�Z���d{��j���HΘ{${��^ �LR�%DR��9c9c��c��5g���������jJ�c��!復R�]�!�g�3�o������<d�����s��5���{H�~I�~I�~ɶ՜2�{�w�&�]��N 3q�ݗg��I��M'�����C���H�	dr�@����������%ZHᛂT��G�=�x����������d��X��?m�)�`�$~J�0c${�K~�d��7��ֻ���Iކ��wQ��K!����NĵpW!����A�<�h����PF�*p�
�jp�=�l/p&�	2��Й��O��7����i�'��\例5���u�E�GW�T���ȪB�rD�yU�r��:�Sh^c����Y��8�yO̹�G^�P�	q�B�ND�QKh?�ܕ�p��Y�L=B̎D'���"N�Є�
-%�����*lK���1_s]�9��Q.�t�aN�BU	u����m�9��
8�09��0*4�B���xS~���ꈷBK+�6�rF����B�/4��BL9�'���Z�¯��u�h_B�/��|���o�k1�
Q�
-82�u���F��滾)$Z��D��T1�Iq�uZ��,��V�sQ���|�p6���O=��w��6yu�d���Bs/���f��)�ڋ*Ԃ�м���<�ܷϘ���*"�����j-�Y�o|�+Uhe��/4���8ߗ��N6��]��%��
kM��h]˲�N��?�����������!T�w�ջ��t�9a��r[j�@���E�=24o��$���s/t�\�}@݋怦W+�(ύy��g�݅HC=��F	\�ጓ�>���%U%��@.2�K�o*��5je"�����3Ai���.��(�8T!U��C*W�=¤!"H�����"T�"1�q&��F�E)TȐB�B�PAJjH�BqV����e��	�D�Q*@"����s�o�K���U��B���G��}����KT0.��� 4�{�v���A�S���RP�5���@�$:m��#L
�Q�ЀF2htI�DHT�%�����A�HdȪR!�SHT��J���"2�oɗ���*t
��*�#K����*�{�/�[�C%�,%L
�^�a��h���6J�
M�Q��M{�Y�J��ȗ#j�h�*�BF�C��.��I�j�As/4PX	U�I�oѾ��N��$_�ߊ�$�!B�9A�`&B:	*�$�r�£Uڝ��Q�Q0Q����7�G��� &/T�x����y�#���}zd���{&�r����"N}��>�S��NA�HԠN7a��߸��V@��FhVW!Q�f��)�h�OT�B��D@�I�P�E��D��'X��D�K���B��@Zw{��7����.�Fr@hV41�==?(5�ET1.�~G���Y@��;h!�Whq�B�0�wp4]��<��-�[rTiD� ��Z�B�[���.b]"4Q		�=r%��SU@����B���P��*!���#R	��r8����[U��:q��׀�Ca[H��U���>���Oձ��.��.��cq�|W����	U/&�4�kZ��(�3ߑ�������}��>�-`�p����B�9 Z!QE;�`߻W�PGdbm_h!��&�{��_�B. �G���,�\�qo4��]����h­�����ȾBU_��&�
t�Gt��K�B��>�~�����l@�l����PE����L�N�}��x��rBIZ�YThA�Յf�q[�=���Q��C�Dh���*:H�+*W�?�!O�	
��K��v�;���.4��2�ꐄ�*^NԘs���!�� j��%M�7�B�" �n�IHCB�V�Z����q	���A�$?�J�/��
NCd����P��5�0��UEG�M̗����������B�L�<=4�Ȉ��tȶ��#`���WE�����/A��Pj���=��`�'���0�;Qar'����b@�(��܉
�;Qcr'Td0(��HTdD��H�(҈E�Q�Qt$�6Q�k5�6��A�$/�jT��&��&�V��tH᳈���d�;�(��zE��P�+h4�B�|O��F��]��� Xa@� C�/G@ŗ#��]�������1�*u|( �����G��C0��,ё j����J��#Dˑ�`�E�zBh\zԊy�h��)j���a��l��$]�2�yR�<T���$49DI�(sM�R�����vD�n*��B�Z�#�a�8������g�a@&.��Q}��8�m"�o�~��A�\hA4_��q�!�zeʃ���ύ8�B�hO_�E�^��qDn���&�,p�kL
B,�&O��N��2J9!
B��M4oo�"Z('�VF�Q�"�n��h�y�A&4�����#�a�B��|Z�D�EGz����>'
vW���}����d�	!��B�H��B˥�Pe(ĥ��
}m�h!����ד)8�ۺ�&��-�(Q�:.�W�w_�AŃ��B�O���g Bg�׋��X4��X@���d.�q���xZw��q�'#4�ȅ�6�qџw<��z3�PG��s�u���~$ |�P��B?,����pW����7NU4�������k}��p+��D!�3Ku�]�<�jb?���X�P�7~�M��[�>V49D���Vq��8�#H�s\D8�L4����\�*N��q�W�4v���SD��UG��#b�-԰�#4p�G�n��h�B�B�;%%+w!�L5���y�����%�U��JD�-4��D���Q(��6�@42$0����P�a�A�z��d �
q�Tr���`Y Ĳ@hW��`�*�#!�	�m��2?��H�=�H~�FE!2P��L�$�ފET(�:�x!vs������:!��	��2?��C���("���z�H<��*pw�P|B��"	]_��J!Xt@h2dpQi��A�[��cKAu�e)� *�ֳ�[�B�N�A�
Z ��HO$x�P�"ν·�_�Al����p�( c_��#�ӄ8���Ih F�{�R�O�x(�Y�����	h�YD�J�q^3��_$j�_B�!C#Ez"��*L�D�GI��x/dTCT��IO02	5���$d���OO�MN��������.��8s>��٧��ְ�~�� ����&߄��t3Q�|Bտ��#�Q�	�<��/ZBS)��l���w�Vh�s !�|B�YU�rM_����ѩ;��U��Q�
]Nt�j_"�5_��2 �s��X��N�����D3Q������9�&�8��ir�<��PÇ�B�K����h9�
ߙ�8���P��w�تUGy���������p�������_;��.l��������� j�~l      �      x������ � �      �   x  x���MO�0����k�|,W�4!.��0��eU�j���Qn���������waX��1�z?��>�K��w��9�i<�|8}�uُ���s(�/(Z��|�1�ϐ���z#�Dlb�aSƐ�6�n�=qOt[��q;���4�	x8���TOG_�,�	ؔ�0�r:�8)�[��XM����K��n�/��'jʱ�kA�,��-DH>�%�� �H�@h>E)�OqT���A�f�#�w+W$/W��9�$oW:Bx��Џ��E�A��PQ
���I����3�- �1��L�����C���}C���5 ?Ohh�7�t������R��Ǽ������l�(P�������� ���!            x������ � �         �  x�uY�r�8]C_�Mv.���3I�*N\q�]��(Zb_R*+���������9x]R�]qU@</��	�46���$���F��ZMSS��}P��8���}W�A��;c�����n��ع��}��.|�˹�Zcˡw}%�ɇ�֪�Ll���i�c�I�[^���լXd��˅~=�f}�1��'57'�)X�Ʌ����4B@���H_9�����v��#��/n��:�u?�}��(�]��Aq�dw�Ҫ�A[2bߏ]��{<�'�6aX�Ǧ�f��H؃ji}����/3v�q������*��,�{��J�~B�ǳ�c���|���a�����<niv¾�	��&���#f��2�@��:z���{5Ba���5T⿓���ͼLH��s�5S��D��Ki�H����iE
5�,A�$cwmK"$�M���Z�5)�=�Go�R޳����ꁴ�
������	n;{��8��A�>zW�l���E])�T�av�{��Y�>��_.����d�H���h�>�k��S�E�qi*� �)���� 
�0f���BtΪ�g�p��S=/��F�;�{.a��+�'����}1�w�?!g}j����]}8N3�ա�W��<�E-K�%p�M��"b�t�_��@J�U���FS�?���w��g��c���P����a�X�ChP�,JX�#���2�N�U7��0B4��lS
�[CkK(�P����Ye¾�sJ�])��`�
}�3g߇	ɥ�\PV~~:�&RL�+�MY��#e�(�e�y�[4rP��ǋH"3���f���˒�D���V��;�("�&X��HD!a�n e�(G2�YDD{��s�kM}H#�G��}j�2c�@���]���~��V ��?,A�i�#�9)�p@,�C;l����8���;������DG�����m�Y%l=��l�n%���~&�"�>O�0�r�\b�#  ����"�]���p�+���@)�p���� >��&^"t��B�$Q����86�A�đ���Đp��wCW�H�kK�Q�����P\�Z�Y�#�-�x���M
3C9�R°�kK�`�t���@���gDXo"�%Fs�u�]w���׍B=���tܘ�n�p�΍�I�А?�#n�#� �/���^%/9Cl�I� &o���u�B+@9�Nk=X�.�A$�(֑�%�eN��������$��Z��zz{(��p̕��	����D�OBi�����.¡�t�t��]��4W�nK��xK�:��j>�w�,W�C�lL�r���"t�a��Ԑy�ʷjjZ{�'��S=!���E��a�0��IS"�A�Ԣ~P��W�׺q��K6��= �}�O�+Z#=��A�&[1n��:����⩯�F�3b-\�[�Y
��<w�H�P�I�V��ˈ���3���rQުѦ�J�xC�Oj�[��`�!�_��a���_�����Ò%7���bE��8hm䶁��C���[��퀒��"�wC�����f�j�m0�d�����[����23��[��w_}���`0_�]�B��d(�%<���Y�Gz�΢&��t�M1a�8�I� 9�H�ڪ������(b����A�ș)i��ˍhHy7<���jy�U��`p'�<��f^�ͳ�{�E8&��{�a��X�͕c�pl�0�:�N��%�h!�@,��LA�â�b����d�I@��U1
�7[��e.�a�2�,�C�M�(��A�V��J9����+��j��;���pԱ�I�mt�:)��\n#J;�͡Z���uy��rY�,�6��1_@a`����x�y�~�T�
3@�o���6#í/�(�[/�C���D�����Pչ� )�P#d,}p�g����k3����.����U:l���C2�Q�J�����gQ���}�U�H�;��h�H���8��J������p)P�Gkn��{l���I����^JO��>`/Cw�)e��t}��h.b��P%\�\	��k�-l�;.�Yś�uׅ��1�\��_;�݄HZ����+@�<^B�X�f�om��	2�C��,F���h��D��J�Hé�C3_�\���P��Ëu��v7A�������e	(�0�����7����ItU�/�v!�a;�|t0��*e�Rӟ��q%Hc������K�*��� Vh�)���@�Pca([�W�Ba��+.�uf���A��Z��Z���T�j1���h}5�F���,p*�i�Kevm!��]�i|<j��~ghڼ�\�+��aQ��]	窻uY��#�Z�����g�V|`k����o��D+�_�(���#(���>j ���P��` c����X�(�<�?��9���c�� 	r���fi ���Ù�*�vT����caj�r��zB�M_{q6Gr�HR��������n�A���}O���o��2-�j�Fx�5!�bXv(��r���H�l[��SӻldplS�_�*h	+�Q3��//o_M@��Є�If���_�{70״lfm��=ت󌤴
�ek̾��M�����qGrz���@���ݿM�2���֥˸���﵁.��'U��9��C(�E_��ߕ\��&�f�p(��?X�r�}w��,|O�ʬ,�+�zh�U��B[Po�Xz���Ą��Ԣ�嘒r� �������CGI�$�VB��Z9�
J;�Z�B�<�̣3$�,[h�ZI�ɌZ��įy��1]"��b3�Cc:4�C�\�R��Q��CM\`�y:<4������Ⱥ�ۙ�&2?����ln���Jp��^�r�譯��� ����'��t��?�H��ޘ�N�W}�T���D}o],���t�-�^�0 ��#̖�����X�9/���*z��2x3�8������D׽@@!;�)�Nm�p��jNl�_��/�X۾�����3��C��VZ��%�%i^j���'��"�q��dՉ�m4�6�J��RB��0E���__�L���v���J_�            x������ � �         9   x�3��N�a���T����J ��ih``�i�@�e��X����7����qqq -��            x������ � �      	   w  x�]�[r�0��ä��w���#�h�.�*�Z ������k��E��]f���W���V��	�L�uF{�������OD�e���#��GF�gA�׵�	���u`5tm�ov�Sh�h�B[�K-�?y����-t�|��������4�Z�e=Z��x�i�i�i������:�u�x;�:��Q���"~��-�?��od�b� �h�����/�;��o�~3�b�D�&�&�&�&�&�7���[�[���y[~���J���5��,�-�-�?�����۸6��F�v�/�6�؟�ɧ����'��v���ױ_��o�<_�̟:�Y�vƷ�x�	����[��/ǅq�G����	|J����9���������?�@��~���?P����P��0?� ���Yϖ�/�� ����x�?��~ޟ���?� �#�?��+��y���q��<��;���^���?z�'�3�F��q�'�߼�cڹ~�~#���7�?ha=�@�빟��������ȏ~�����B����
ݠ;�>��|��������p=B~�_G�u������<�迎�qG�u�_w��:�ǟ���������bOW�����'����=)�ֿ��~��V      
      x�34�425�227 �1z\\\ "         �   x�U���0DϳU�1�&RA.olK�F���PCN��1�	N��f45�!9q7��ZTf�)x6��:{�Ζr��Y�Դ��M�}g��b�J�}���X{p�y���?�#,{��z�>�����%=[�9�            x������ � �            x�3�2�2��26�26����� G�         �  x�һu�0D����������A�����U������z�>Z����w۷ݯ�#���,�lǳ�Xn����\�r�t�+]�n�n��������������h��5uS7uK�tK�tK��G�-��-��m��m��m�~�M�u�a?��;��;��;��;��3E������������ӽ-��t'N9���q��v�{��xL���麮�.�袋.:aaaaaaaaaa�������������������aaaaaaaaaŠ��m}�Y�vN+�����e��پl_��w��e�yټl^6/�����e�yټl^6����({��Zk��ꊱ            x��Z�r�8]�_�e�ʭI=�vb;=�3�פfI���H�c�Lo���*;�؜sA��3S��H����s��Ǎ+L��ݘ�n��/�,V�e�������H�-��P=غv�Ԫ�����/+������X]��en��ǳL�S����ݣ]׮,08햻l���ef޺W�ںB��|���z�����Y���L33�_����qV�Tza��69�Ҿ�}/*�lv������<�lO_{W�p�ۅR�l��ʗ��m���Y�c֦X`��YV�u��-�j�sNV�_��+/������{��g��U]{�����ޮ�s��\_3�`p��eR���u�&�Y5��J_;�9���lQ�~�V;�}_r�z��:�a��3|�_���S��L�\٥/�6�n�w�եls���.�V����+uI��8�Kn�����p��)��Mf*��������aKf���緍��A��՞�t��ǟk�w�����uշ��%���j�n�	�jp�N�>�s��&��r���ǆ���+�K�B{4���_a�5��A�WֺUa�H=siq̝�U�U������W��x���j�T�,�Û�K�!d;��)v	;���L�a)��Ch�����m�ӿ�sJ��}��­�zu~qd�)g}%S���0Et�'�yu����)�6��V������'��_��-�񣆳UX�مDP)1�R!��3W�g�`��Wn��M��7�d���D���
�ra�4���s�U_��;<�Y���5R�Cy�É[ ��ˬ�fQ�GL�_w?��n��1�h#k�Q���<�m�������gn��#����V6���/���~qe��87@��̲��|Y��)l��`�gHlW,z��6�������iYULl6QF���7��ꡬmզJ����~�й��BZN��-��!+������Y��]��+�/�耘�Io`!����̪+W|#.�i0a�ϊ��,<l��b�
/��v|���o�F��/e�d&[0	J�|�s�9C�@�̜�q��Z2p?&�u�oٮ�v��g3ۂ,s��S3-�^O_4�E}�/�o2�Țe�!���p���>���V����q�)� ���q����(7��{i���la�� ��~6::��7�(_,NJ�кY�C�����(��u��plܣ�"�y�(����>�άE󦭍)�S �*�B
�d�Υ*u8�}*7�u�Ս���=��6.����/�&�h������jKUc���b�38~a�W���!�8��}�4#�\�MO�<{��m�^�Y��-�eӹk� �VJ�N�`�D]�)�M�΢��ȍ ��m���W �f��i����m��%b����d��Bk���/�(�g��g��r�A�Lp�~+�U�Tu������	.r�яX �*�J43yn�.��<h��8�K�"�k+�j�
EKVM���l�[��T}pY�w�>��{��[�������/@�X5s5��`���m�װ��{6�OL�c28��k�-*Va��������'��s
���a�n���h�%�����P�H�;�~8R�ayi�>9������ �&q��%yS�uF�zp��)���R+���)��U珆�	QOo�� y��h�n�g�y4Q��*�����`�h��:�40�>�e�[|�]���q��8������h[^�I������MXk_��\(:�y�������>��i���[���'�^�lS�)�8�Ǆ>�N��$"�!0�$FD�z��~d[�t� �k�0T��gcj��9-�L���0�&S1ǌ��8<@����'0�t�R�ę�:k
�B��̈́�b`�ҹZ���t��
����D6�`B��ǩD�|���~��tOԏ��[԰9��cb�J�6�U�Of���
З���Cu�dfk�>4��W�����;q^ԟ�� S�����L^�E�?�����dosPJv"u�R�df}��|�$"?��+��&�B/B���-Z���u�)��a"T���-m�=[�bn*X#�@�^mv���|6��ƯO�p~�@�3��@��0<"��Ƿ�*72���%O�_�3����_!�LQ���c���ұ�-�6��*��)maЧZ1AJ�b윌g�Z�̆���d16&�'F 6�@�j��t��o!gKS<H�#Q����6�<㝦f{��NY/���f��3���26=�l�	��?�U�&��,�L���J΍Xj���@�0B�#��lڋ�p��y	,G�\FCH�����]�ηBi���/IxK|2�%U���J��nJۃc�eV��#�$�x/	���ca��H�����|�P�4�Q.(�0|)��@�ac��{KIE�Aa%��(v��)D�p5 Jo!�UN(\��(�\L�b��y֮��W�{�2��H�`��
��6:Ғ���7YKOh<f�##	�%�2�8dj ����	�*�F�l	O��?��+RWP-��B�����8!��v�$X��1~�[?��*��� �3��W@���<Q��.�װ!�DV��?�S���Vr���R�r4+J�cw��ߛz����$~u��s�{��}F.�6PA��uo����o������Ϊ.�-xG ��F�Pib�ZJU#?���ϗD8q��QZ0�A��I���ȕ�o�yQ�FXw��s���!Q�Q�j�{G��z�ao�g�Q�A�n����ɞF	m��yQ�v	~K���@fj(}��LJS�r��6���0��
�<���n�P�988���_[&(�G�3A��o�˙�l�T6R/� ���H�	�k�yB����5�Ƨ��u#��N���T����p�N�W;)��
@�v����a��uB9(,DƬe|��v&{��2_P��;H�B����C,���e G��ÃB��(��e	}��l8���@? V�N'D'�C�<�	h���~�o�y�.
��T4�Fak��؞��/A��F�W6#^Y�Kϩ�-��A�K/9�04Eۭڨ� .0���sȊ�*�!��5%��"G�Y��_[���ȱ���5��`?j�e��HF�$����#(9t�<�	�eVd���x�Һ�>0��h�n}$�prN(��J�P�y����m�w���\O�́���g���Q�q
���Htk�d\��bx��-훙��O�DW-@�����[�����(TG'�"�ձ� �������I H��:��( \�Y��m��	Z���Vcw�I4^+��ʌ����/|B�T5E�` 
�Z��P̺�C�t�#�_'&Iw��7^X�3�6���1p�N�� 
�DgUfv 5Ưyۚ��sg�5.{��|�M`i�m
RK@(�j�F�+Y5��/�����S@7�^`�
z,��'X�/���c���W�\xH'X�d�Gz�]�<�2(�*�8�ҏ�	��R#��S�\��YJ2 <1t�
�U���A-�� �V#���f������V2�6��an%f��I�%�HBƉ�A��u�p�nf��H�P�n��{�1u����S�{x����XA���%>��>9������*�����f���OJ��c�ciJ��?ңVTt��1 ��t��R����d��|%��A�t�q
Vg��Z�|�0| A#a�9㩀
�%���~�IJ����$���FP=�*m6��y �oͦ��󔀼�g'��mhkFMO�N����ɠk���$��^�����J��I<��^��񠋓xrrU����H� ���B!�!Ð����^��?�K�0S�r�׽��G1��&@���ח&[Y��	����9bO�?�+��QȄM%��fU����ʣPڡ�¶���G}z�z��WOOW���ǫ�v�_i$�����'��N�7��tw��|��BN�x%H���:~��ui�֛,<��Ƭ\m2����iK��6�l\��{^����V����|E
*������ @  �+(��<HI�ܠ�g�B?S1�
��֝�$�t �oԏ?�@c�H����w�J�|a۾e�k���{����Ƀ��R��)�ko�W~�R9�� ��-AYˇ+���(|�(�w@�ܠs�^,��D>��b�=�������"�C�aA*�B��w�9�ޑg2���J����A&�!���������&�S�mZ���AH�![�&�&D`������/N�f Z�� �0�]�^�}_�y��ena�@T3�3��� Y$�O��+><��S����H���79�Τ�}$Y.6��Z�������� �q_         �   x��ˍ1��,�!�۽l�u�]FB�p���w��w<��|�#a�A$QDC�H#u\�E6��Q�2*(�STSC-u���:i��顗>�1�8L2���,�Xc���f�3ι�kn���SLq%�RF�So�M��7���6֊v�	11)191�NbLf"V����C����}�=D�QG}��x�������+;�         �   x�m�Q�� D��aV�.�'�wO���c���iD"��<��Y[xg���?�����p��ܗ,"$d��1�k�DqFKt�i�r��/~o$���"D�Z'��;�
E�M^�틪����r�^�.��q��`�W����v�@�(8l�����c�+Y�(f� ��7y!Ȳ���M/�V0���{��!���>ٻю����N��F�x�h&�����l�     