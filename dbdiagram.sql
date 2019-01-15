
CREATE TABLE `preg_answer`(
	`user_id` int ,
	`question_id` int ,
	`questiondate` datetime ,
	`title` nvarchar(1024) ,
	`content` text 
);

CREATE TABLE `preg_appointment`(
	`id` int ,
	`name` nvarchar(1024) ,
	`contact_name` nvarchar(1024) ,
	`profession_id` int ,
	`appointment_date` datetime ,
	`appointment_time` datetime ,
	`my_weight_type_id` int ,
	`weight_in_st` float ,
	`sync_to_calendar` int ,
	`note` nvarchar(1024) ,
	`user_id` int ,
	`appointment_type_id` int
);
GO
/****** Object:  Table `preg_appointment_measurement`    Script Date: 1/15/2019 2:10:00 PM ******/

GO

GO
CREATE TABLE `preg_appointment_measurement`(
	`id` int ,
	`appointment_id` int ,
	`bp_dia` int ,
	`bp_sys` int ,
	`baby_heart` int ,

);
GO
/****** Object:  Table `preg_appointment_type`    Script Date: 1/15/2019 2:10:00 PM ******/

GO

GO
CREATE TABLE `preg_appointment_type`(
	`id` int ,
	`type` nvarchar(1024) ,
 CONSTRAINT `PK__preg_app__3213E83FA1D0CB72` PRIMARY KEY CLUSTERED 
(
	`id` ASC
)
) ON `PRIMARY`;
GO
/****** Object:  Table `preg_auth`    Script Date: 1/15/2019 2:10:00 PM ******/

GO

GO
CREATE TABLE `preg_auth`(
	`id` int ,
	`user_id` int ,
	`token` varchar(1024) ,
	`valid_to` varchar(1024) ,
PRIMARY KEY CLUSTERED 
(
	`id` ASC
)
) ON `PRIMARY`;
GO
/****** Object:  Table `preg_baby_name`    Script Date: 1/15/2019 2:10:00 PM ******/

GO

GO
CREATE TABLE `preg_baby_name`(
	`id` int ,
	`country_id` int ,
	`gender_id` int ,
	`name` nvarchar(1024) ,
	`custom_baby_name_by_user_id` int ,
	`order` int ,
 CONSTRAINT `PK__preg_bab__3213E83F3A7B935D` PRIMARY KEY CLUSTERED 
(
	`id` ASC
)
) ON `PRIMARY`;
GO
/****** Object:  Table `preg_contact_us`    Script Date: 1/15/2019 2:10:00 PM ******/

GO

GO
CREATE TABLE `preg_contact_us`(
	`id` int ,
	`user_id` int ,
	`email` varchar(300) ,
	`message` nvarchar(max) ,
 CONSTRAINT `PK__preg_cot__3213E83FBC08C8FF` PRIMARY KEY CLUSTERED 
(
	`id` ASC
)
) ON `PRIMARY` TEXTIMAGE_ON `PRIMARY`;
GO
/****** Object:  Table `preg_contraction`    Script Date: 1/15/2019 2:10:00 PM ******/

GO

GO
CREATE TABLE `preg_contraction`(
	`id` int ,
	`user_id` int ,
	`date_time` datetime ,
	`duration` datetime ,
	`interval` datetime ,
 CONSTRAINT `PK_preg_contraction` PRIMARY KEY CLUSTERED 
(
	`id` ASC
)
) ON `PRIMARY`;
GO
/****** Object:  Table `preg_country`    Script Date: 1/15/2019 2:10:00 PM ******/

GO

GO
CREATE TABLE `preg_country`(
	`id` int ,
	`name` nvarchar(1024) ,
 CONSTRAINT `PK__preg_cou__3213E83F6277CFEF` PRIMARY KEY CLUSTERED 
(
	`id` ASC
)
) ON `PRIMARY`;
GO
/****** Object:  Table `preg_customer_response`    Script Date: 1/15/2019 2:10:00 PM ******/

GO

GO
CREATE TABLE `preg_customer_response`(
	`user_id` int ,
	`content` nvarchar(1024) ,
	`time` datetime ,
	`answer_user_id` int ,
	`answer_date` datetime ,
	`answer_content` nvarchar(1024) ,
 CONSTRAINT `PK_preg_customer_response` PRIMARY KEY CLUSTERED 
(
	`user_id` ASC
)
) ON `PRIMARY`;
GO
/****** Object:  Table `preg_daily`    Script Date: 1/15/2019 2:10:00 PM ******/

GO

GO
CREATE TABLE `preg_daily`(
	`id` int ,
	`title` nvarchar(1024) ,
	`highline_image` varchar(1024) ,
	`short_description` nvarchar(1024) ,
	`description` nvarchar(max) ,
	`daily_blog` nvarchar(1024) ,
 CONSTRAINT `PK__preg_dai__3213E83FA45B8793` PRIMARY KEY CLUSTERED 
(
	`id` ASC
)
) ON `PRIMARY` TEXTIMAGE_ON `PRIMARY`;
GO
/****** Object:  Table `preg_daily_interact`    Script Date: 1/15/2019 2:10:00 PM ******/

GO

GO
CREATE TABLE `preg_daily_interact`(
	`daily_id` int ,
	`user_id` int ,
	`like` int ,
	`comment` nvarchar(1024) ,
	`share` int ,
	`notification` int ,
	`status` int ,
 CONSTRAINT `PK_preg_daily_interact` PRIMARY KEY CLUSTERED 

) ;
GO
/****** Object:  Table `preg_faq`    Script Date: 1/15/2019 2:10:00 PM ******/

GO

GO
CREATE TABLE `preg_faq`(
	`id` int ,
	`question` nvarchar(1024) ,
	`status` int ,
 CONSTRAINT `PK_preg_faq` PRIMARY KEY CLUSTERED 
(
	`id` ASC
)
) ON `PRIMARY`;
GO
/****** Object:  Table `preg_faq_answer`    Script Date: 1/15/2019 2:10:01 PM ******/

GO

GO
CREATE TABLE `preg_faq_answer`(
	`id` int ,
	`faq_id` int ,
	`answer_content` nvarchar(1024) ,
 CONSTRAINT `PK_preg_faq_answer` PRIMARY KEY CLUSTERED 
(
	`id` ASC
)
) ON `PRIMARY`;
GO
/****** Object:  Table `preg_gender`    Script Date: 1/15/2019 2:10:01 PM ******/

GO

GO
CREATE TABLE `preg_gender`(
	`id` int ,
	`gender` nvarchar(1024) ,
 CONSTRAINT `PK__preg_gen__3213E83F49DEFC19` PRIMARY KEY CLUSTERED 
(
	`id` ASC
)
) ON `PRIMARY`;
GO
/****** Object:  Table `preg_guides`    Script Date: 1/15/2019 2:10:01 PM ******/

GO

GO
CREATE TABLE `preg_guides`(
	`id` int ,
	`page_id` int ,
	`guides_type_id` int ,
PRIMARY KEY CLUSTERED 
(
	`id` ASC
)
) ON `PRIMARY`;
GO
/****** Object:  Table `preg_guides_type`    Script Date: 1/15/2019 2:10:01 PM ******/

GO

GO
CREATE TABLE `preg_guides_type`(
	`id` int ,
	`type` nvarchar(1024) ,
 CONSTRAINT `PK__preg_gui__3213E83F278A0EC4` PRIMARY KEY CLUSTERED 
(
	`id` ASC
)
) ON `PRIMARY`;
GO
/****** Object:  Table `preg_help`    Script Date: 1/15/2019 2:10:01 PM ******/

GO

GO
CREATE TABLE `preg_help`(
	`id` int ,
	`help_category_id` int ,
	`image` varchar(1024) ,
	`description` nvarchar(max) ,
 CONSTRAINT `PK__preg_hel__3213E83F4020787C` PRIMARY KEY CLUSTERED 
(
	`id` ASC
)
) ON `PRIMARY` TEXTIMAGE_ON `PRIMARY`;
GO
/****** Object:  Table `preg_help_category`    Script Date: 1/15/2019 2:10:01 PM ******/

GO

GO
CREATE TABLE `preg_help_category`(
	`id` int ,
	`name` nvarchar(1024) ,
	`highline_image` varchar(1024) ,
	`order` int ,
 CONSTRAINT `PK__preg_hel__3213E83F28C7F412` PRIMARY KEY CLUSTERED 
(
	`id` ASC
)
) ON `PRIMARY`;
GO
/****** Object:  Table `preg_hospital_bag_item`    Script Date: 1/15/2019 2:10:01 PM ******/

GO

GO
CREATE TABLE `preg_hospital_bag_item`(
	`id` int ,
	`name` nvarchar(1024) ,
	`type` int ,
	`custom_item_by_user_id` int ,
 CONSTRAINT `PK_preg_hospital_bag_item` PRIMARY KEY CLUSTERED 
(
	`id` ASC
)
) ON `PRIMARY`;
GO
/****** Object:  Table `preg_image`    Script Date: 1/15/2019 2:10:01 PM ******/

GO

GO
CREATE TABLE `preg_image`(
	`id` int ,
	`image_type_id` int ,
	`image` varchar(max) ,
	`week_id` int ,
 CONSTRAINT `PK__preg_ima__3213E83FF94F2BD6` PRIMARY KEY CLUSTERED 
(
	`id` ASC
)
) ON `PRIMARY` TEXTIMAGE_ON `PRIMARY`;
GO
/****** Object:  Table `preg_image_type`    Script Date: 1/15/2019 2:10:01 PM ******/

GO

GO
CREATE TABLE `preg_image_type`(
	`id` int ,
	`type` nvarchar(1024) ,
 CONSTRAINT `PK__preg_ima__3213E83F39314544` PRIMARY KEY CLUSTERED 
(
	`id` ASC
)
) ON `PRIMARY`;
GO
/****** Object:  Table `preg_kick_result`    Script Date: 1/15/2019 2:10:01 PM ******/

GO

GO
CREATE TABLE `preg_kick_result`(
	`id` int ,
	`kick_order` int ,
	`kick_time` datetime ,
	`elapsed_time` datetime ,
 CONSTRAINT `PK_preg_kick_result` PRIMARY KEY CLUSTERED 
(
	`id` ASC
)
) ON `PRIMARY`;
GO
/****** Object:  Table `preg_medical_package_test`    Script Date: 1/15/2019 2:10:01 PM ******/

GO

GO
CREATE TABLE `preg_medical_package_test`(
	`medical_service_package_id` int ,
	`medical_test_id` int ,
 CONSTRAINT `PK_preg_medical_package_test` PRIMARY KEY CLUSTERED 
) ON `PRIMARY`;
GO
/****** Object:  Table `preg_medical_service_package`    Script Date: 1/15/2019 2:10:01 PM ******/

GO

GO
CREATE TABLE `preg_medical_service_package`(
	`id` int ,
	`title` nvarchar(1024) ,
	`description` nvarchar(1024) ,
	`content` nvarchar(1024) ,
	`discount` float ,
	`execution_department` nvarchar(1024) ,
	`address` nvarchar(1024) ,
	`execution_time` datetime ,
	`place` int ,
 CONSTRAINT `PK_preg_medical_service_package` PRIMARY KEY CLUSTERED 
(
	`id` ASC
)
) ON `PRIMARY`;
GO
/****** Object:  Table `preg_medical_test`    Script Date: 1/15/2019 2:10:01 PM ******/

GO

GO
CREATE TABLE `preg_medical_test`(
	`id` int ,
	`title` nvarchar(1024) ,
	`type` nvarchar(1024) ,
	`price` float ,
 CONSTRAINT `PK_preg_medical_test` PRIMARY KEY CLUSTERED 
(
	`id` ASC
)
) ON `PRIMARY`;
GO
/****** Object:  Table `preg_my_belly`    Script Date: 1/15/2019 2:10:01 PM ******/

GO

GO
CREATE TABLE `preg_my_belly`(
	`id` int ,
	`image` varchar(1024) ,
	`month` int ,
	`user_id` int ,
PRIMARY KEY CLUSTERED 
(
	`id` ASC
)
) ON `PRIMARY`;
GO
/****** Object:  Table `preg_my_birth_plan`    Script Date: 1/15/2019 2:10:02 PM ******/

GO

GO
CREATE TABLE `preg_my_birth_plan`(
	`my_birth_plan_item_id` int ,
	`user_id` int ,
 CONSTRAINT `PK_preg_my_birth_plan` PRIMARY KEY CLUSTERED 

) ON `PRIMARY`;
GO
/****** Object:  Table `preg_my_birth_plan_item`    Script Date: 1/15/2019 2:10:02 PM ******/

GO

GO
CREATE TABLE `preg_my_birth_plan_item`(
	`id` int ,
	`my_birth_plan_type_id` int ,
	`item_content` nvarchar(1024) ,
	`custom_item_by_user_id` int ,
 CONSTRAINT `PK_preg_my_birth_plan_items` PRIMARY KEY CLUSTERED 
(
	`id` ASC
)
) ON `PRIMARY`;
GO
/****** Object:  Table `preg_my_birth_plan_type`    Script Date: 1/15/2019 2:10:02 PM ******/

GO

GO
CREATE TABLE `preg_my_birth_plan_type`(
	`id` int ,
	`type` nvarchar(1024) ,
	`type_icon` nvarchar(1024) ,
 CONSTRAINT `PK__preg_my___3213E83F519CAFD1` PRIMARY KEY CLUSTERED 
(
	`id` ASC
)
) ON `PRIMARY`;
GO
/****** Object:  Table `preg_my_weight`    Script Date: 1/15/2019 2:10:02 PM ******/

GO

GO
CREATE TABLE `preg_my_weight`(
	`id` int ,
	`user_id` int ,
	`my_weight_type_id` int ,
	`start_date` datetime ,
	`pre_pregnancy_weight` float ,
	`current_weight` float ,
PRIMARY KEY CLUSTERED 
(
	`id` ASC
)
) ON `PRIMARY`;
GO
/****** Object:  Table `preg_my_weight_unit`    Script Date: 1/15/2019 2:10:02 PM ******/

GO

GO
CREATE TABLE `preg_my_weight_unit`(
	`id` int ,
	`unit` nvarchar(1024) ,
 CONSTRAINT `PK__preg_my___3213E83F86D336A4` PRIMARY KEY CLUSTERED 
(
	`id` ASC
)
) ON `PRIMARY`;
GO
/****** Object:  Table `preg_page`    Script Date: 1/15/2019 2:10:02 PM ******/

GO

GO
CREATE TABLE `preg_page`(
	`id` int ,
	`title` nvarchar(1024) ,
	`content` nvarchar(1024) ,
	`page_image` nvarchar(1024) ,
 CONSTRAINT `PK__preg_pag__3213E83FB991BE41` PRIMARY KEY CLUSTERED 
(
	`id` ASC
)
) ON `PRIMARY`;
GO
/****** Object:  Table `preg_phone`    Script Date: 1/15/2019 2:10:02 PM ******/

GO

GO
CREATE TABLE `preg_phone`(
	`id` int ,
	`profession_id` int ,
	`phone_number` varchar(1024) ,
	`user_id` int ,
PRIMARY KEY CLUSTERED 
(
	`id` ASC
)
) ON `PRIMARY`;
GO
/****** Object:  Table `preg_pregnancy`    Script Date: 1/15/2019 2:10:02 PM ******/

GO

GO
CREATE TABLE `preg_pregnancy`(
	`id` int ,
	`user_id` int ,
	`baby_gender` int ,
	`due_date` datetime ,
	`show_week` int ,
	`pregnancy_loss` int ,
	`baby_already_born` int ,
	`date_of_birth` datetime ,
	`weeks_pregnant` int ,
PRIMARY KEY CLUSTERED 
(
	`id` ASC
)
) ON `PRIMARY`;
GO
/****** Object:  Table `preg_profession`    Script Date: 1/15/2019 2:10:02 PM ******/

GO

GO
CREATE TABLE `preg_profession`(
	`user_id` int ,
	`profession_type_id` int ,
	`status` int ,
 CONSTRAINT `PK_preg_profession` PRIMARY KEY CLUSTERED 

) ON `PRIMARY`;
GO
/****** Object:  Table `preg_profession_type`    Script Date: 1/15/2019 2:10:02 PM ******/

GO

GO
CREATE TABLE `preg_profession_type`(
	`id` int ,
	`profession_type` nvarchar(1024) ,
 CONSTRAINT `PK__preg_pro__3213E83FA1581D8A` PRIMARY KEY CLUSTERED 
(
	`id` ASC
)
) ON `PRIMARY`;
GO
/****** Object:  Table `preg_question`    Script Date: 1/15/2019 2:10:02 PM ******/

GO

GO
CREATE TABLE `preg_question`(
	`id` int ,
	`question_type_id` int ,
	`content` nvarchar(1024) ,
	`custom_question_by_user_id` int ,
 CONSTRAINT `PK__preg_que__3213E83F5C5A14F5` PRIMARY KEY CLUSTERED 
(
	`id` ASC
)
) ON `PRIMARY`;
GO
/****** Object:  Table `preg_question_type`    Script Date: 1/15/2019 2:10:02 PM ******/

GO

GO
CREATE TABLE `preg_question_type`(
	`id` int ,
	`type` nvarchar(1024) ,
 CONSTRAINT `PK_preg_question_type` PRIMARY KEY CLUSTERED 
(
	`id` ASC
)
) ON `PRIMARY`;
GO
/****** Object:  Table `preg_setting`    Script Date: 1/15/2019 2:10:02 PM ******/

GO

GO
CREATE TABLE `preg_setting`(
	`id` int ,
	`reminders` bit ,
	`length_units` bit ,
	`weight_unit` int ,
	`user_id` int ,
	`revoke_consent` int ,
PRIMARY KEY CLUSTERED 
(
	`id` ASC
)
) ON `PRIMARY`;
GO
/****** Object:  Table `preg_shopping_category`    Script Date: 1/15/2019 2:10:02 PM ******/

GO

GO
CREATE TABLE `preg_shopping_category`(
	`id` int ,
	`title` nvarchar(1024) ,
	`status` int ,
 CONSTRAINT `PK_preg_shopping_category` PRIMARY KEY CLUSTERED 
(
	`id` ASC
)
) ON `PRIMARY`;
GO
/****** Object:  Table `preg_shopping_item`    Script Date: 1/15/2019 2:10:02 PM ******/

GO

GO
CREATE TABLE `preg_shopping_item`(
	`id` int ,
	`item_name` nvarchar(1024) ,
	`custom_item_by_user_id` int ,
	`category_id` int ,
	`status` int ,
 CONSTRAINT `PK_preg_shopping_item` PRIMARY KEY CLUSTERED 
(
	`id` ASC
)
) ON `PRIMARY`;
GO
/****** Object:  Table `preg_size_guide`    Script Date: 1/15/2019 2:10:02 PM ******/

GO

GO
CREATE TABLE `preg_size_guide`(
	`id` int ,
	`image` varchar(1024) ,
	`title` nvarchar(1024) ,
	`description` nvarchar(1024) ,
	`week_id` int ,
	`length` float ,
	`weight` float ,
	`type` int ,
 CONSTRAINT `PK__preg_siz__3213E83F27AE4616` PRIMARY KEY CLUSTERED 
(
	`id` ASC
)
) ON `PRIMARY`;
GO
/****** Object:  Table `preg_social_type`    Script Date: 1/15/2019 2:10:02 PM ******/

GO

GO
CREATE TABLE `preg_social_type`(
	`id` int ,
	`type` nvarchar(1024) ,
 CONSTRAINT `PK_preg_social_type` PRIMARY KEY CLUSTERED 
(
	`id` ASC
)
) ON `PRIMARY`;
GO
/****** Object:  Table `preg_time_frame`    Script Date: 1/15/2019 2:10:02 PM ******/

GO

GO
CREATE TABLE `preg_time_frame`(
	`id` int ,
	`frame_title` nvarchar(1024) ,
 CONSTRAINT `PK__preg_tim__3213E83FDB4ED0ED` PRIMARY KEY CLUSTERED 
(
	`id` ASC
)
) ON `PRIMARY`;
GO
/****** Object:  Table `preg_time_line`    Script Date: 1/15/2019 2:10:02 PM ******/

GO

GO
CREATE TABLE `preg_time_line`(
	`id` int ,
	`week_id` int ,
	`title` nvarchar(1024) ,
	`image` varchar(1024) ,
	`position` nvarchar(1024) ,
	`time_frame_id` int ,
 CONSTRAINT `PK__preg_tim__3213E83FC723BD5A` PRIMARY KEY CLUSTERED 
(
	`id` ASC
)
) ON `PRIMARY`;
GO
/****** Object:  Table `preg_todo`    Script Date: 1/15/2019 2:10:02 PM ******/

GO

GO
CREATE TABLE `preg_todo`(
	`id` int ,
	`day_id` int ,
	`title` nvarchar(1024) ,
	`custom_task_by_user_id` int ,
 CONSTRAINT `PK__preg_tod__3213E83F5B2A8E38` PRIMARY KEY CLUSTERED 
(
	`id` ASC
)
) ON `PRIMARY`;
GO
/****** Object:  Table `preg_upgrade`    Script Date: 1/15/2019 2:10:02 PM ******/

GO

GO
CREATE TABLE `preg_upgrade`(
	`id` int ,
	`user_id` int ,
	`version` varchar(200) ,
PRIMARY KEY CLUSTERED 
(
	`id` ASC
)
) ON `PRIMARY`;
GO
/****** Object:  Table `preg_user`    Script Date: 1/15/2019 2:10:02 PM ******/

GO

GO
CREATE TABLE `preg_user`(
	`id` int ,
	`password` varchar(1024) ,
	`phone` varchar(1024) ,
	`social_type_id` int ,
	`first_name` nvarchar(1024) ,
	`last_name` nvarchar(1024) ,
	`you_are_the` nvarchar(1024) ,
	`location` nvarchar(1024) ,
	`status` nvarchar(1024) ,
	`avarta` nvarchar(1024) ,
	`email` nvarchar(1024) ,
 CONSTRAINT `PK__preg_use__3213E83F3F5A9B5F` PRIMARY KEY CLUSTERED 
(
	`id` ASC
)
) ON `PRIMARY`;
GO
/****** Object:  Table `preg_user_baby_name`    Script Date: 1/15/2019 2:10:02 PM ******/

GO

GO
CREATE TABLE `preg_user_baby_name`(
	`user_id` int ,
	`baby_name_id` int ,
 CONSTRAINT `PK_preg_user_baby_name_1` PRIMARY KEY CLUSTERED 

) ON `PRIMARY`;
GO
/****** Object:  Table `preg_user_hospital_bag_item`    Script Date: 1/15/2019 2:10:02 PM ******/

GO

GO
CREATE TABLE `preg_user_hospital_bag_item`(
	`user_id` int ,
	`hospital_bag_item_id` int ,
	`status` int ,
 CONSTRAINT `PK_preg_user_hospital_bag_item` PRIMARY KEY CLUSTERED 

) ON `PRIMARY`;
GO
/****** Object:  Table `preg_user_kick_history`    Script Date: 1/15/2019 2:10:02 PM ******/

GO

GO
CREATE TABLE `preg_user_kick_history`(
	`user_id` int ,
	`kick_result_id` int ,
	`kick_date` datetime ,
	`duration` datetime ,
 CONSTRAINT `PK_preg_user_kick_history` PRIMARY KEY CLUSTERED 

) ON `PRIMARY`;
GO
/****** Object:  Table `preg_user_medical_service_package`    Script Date: 1/15/2019 2:10:02 PM ******/

GO

GO
CREATE TABLE `preg_user_medical_service_package`(
	`user_id` int ,
	`medical_service_package_id` int ,
	`time` datetime ,
	`description` nvarchar(1024) ,
	`total_cost` float ,
	`status` int ,
	`payment_method` nvarchar(1024) ,
	`already_paid` int ,
 CONSTRAINT `PK_preg_user_medical_service_package` PRIMARY KEY CLUSTERED 

) ON `PRIMARY`;
GO
/****** Object:  Table `preg_user_shopping_cart`    Script Date: 1/15/2019 2:10:02 PM ******/

GO

GO
CREATE TABLE `preg_user_shopping_cart`(
	`user_id` int ,
	`shopping_item_id` int ,
	`status` int ,
 CONSTRAINT `PK_preg_user_shopping_cart` PRIMARY KEY CLUSTERED 

) ON `PRIMARY`;
GO
/****** Object:  Table `preg_user_todo`    Script Date: 1/15/2019 2:10:03 PM ******/

GO

GO
CREATE TABLE `preg_user_todo`(
	`user_id` int ,
	`todo_id` int ,
	`status` int ,
 CONSTRAINT `PK_preg_user_todo_1` PRIMARY KEY CLUSTERED 

) ON `PRIMARY`;
GO
/****** Object:  Table `preg_week`    Script Date: 1/15/2019 2:10:03 PM ******/

GO

GO
CREATE TABLE `preg_week`(
	`id` int ,
	`length` float ,
	`weight` float ,
	`title` nvarchar(1024) ,
	`short_description` nvarchar(1024) ,
	`description` nvarchar(1024) ,
	`daily_relation` nvarchar(1024) ,
 CONSTRAINT `PK__preg_wee__3213E83F7F6AAC20` PRIMARY KEY CLUSTERED 

) ON `PRIMARY`;
GO
/****** Object:  Table `preg_weekly_interact`    Script Date: 1/15/2019 2:10:03 PM ******/

GO

GO
CREATE TABLE `preg_weekly_interact`(
	`week_id` int ,
	`user_id` int ,
	`like` int ,
	`comment` nvarchar(max) ,
	`photo` varchar(1024) ,
	`share` nvarchar(1024) ,
	`notification` int ,
	`status` int ,
 CONSTRAINT `PK_preg_weekly_interact` PRIMARY KEY CLUSTERED 

) ON `PRIMARY` TEXTIMAGE_ON `PRIMARY`;
GO
ALTER TABLE `preg_answer` ADD FOREIGN KEY (`question_id`) REFERENCES `preg_question` (`id`);

GO
ALTER TABLE `preg_answer` CHECK CONSTRAINT `FK__preg_answ__quest__01142BA1`
GO
ALTER TABLE `preg_answer` ADD FOREIGN KEY (`user_id`) REFERENCES `preg_user` (`id`);
GO
ALTER TABLE `preg_answer` CHECK CONSTRAINT `FK__preg_answ__user___00200768`
GO
ALTER TABLE `preg_appointment` ADD FOREIGN KEY (`appointment_type_id`) REFERENCES `preg_appointment_type` (`id`);
GO
ALTER TABLE `preg_appointment` CHECK CONSTRAINT `FK__preg_appo__appoi__6E01572D`
GO
ALTER TABLE `preg_appointment` ADD FOREIGN KEY (`my_weight_type_id`) REFERENCES `preg_my_weight_unit` (`id`);
GO
ALTER TABLE `preg_appointment` CHECK CONSTRAINT `FK__preg_appo__my_we__6FE99F9F`
GO
ALTER TABLE `preg_appointment` ADD FOREIGN KEY (`profession_id`) REFERENCES `preg_profession_type` (`id`);
GO
ALTER TABLE `preg_appointment` CHECK CONSTRAINT `FK__preg_appo__profe__6EF57B66`
GO
ALTER TABLE `preg_appointment` ADD FOREIGN KEY (`user_id`) REFERENCES `preg_user` (`id`);
GO
ALTER TABLE `preg_appointment` CHECK CONSTRAINT `FK__preg_appo__user___70DDC3D8`
GO
ALTER TABLE `preg_appointment_measurement` ADD FOREIGN KEY (`appointment_id`) REFERENCES `preg_appointment` (`id`);
GO
ALTER TABLE `preg_appointment_measurement` CHECK CONSTRAINT `FK_preg_appointment_measurement_preg_appointment`
GO
ALTER TABLE `preg_auth` ADD FOREIGN KEY (`user_id`) REFERENCES `preg_user` (`id`);
GO
ALTER TABLE `preg_auth` CHECK CONSTRAINT `FK__preg_auth__user___66603565`
GO
ALTER TABLE `preg_baby_name` ADD FOREIGN KEY (`gender_id`) REFERENCES `preg_gender` (`id`);
GO
ALTER TABLE `preg_baby_name` CHECK CONSTRAINT `FK__preg_baby__preg___619B8048`
GO
ALTER TABLE `preg_baby_name` ADD FOREIGN KEY (`country_id`) REFERENCES `preg_country` (`id`);
GO
ALTER TABLE `preg_baby_name` CHECK CONSTRAINT `FK__preg_baby__preg___628FA481`
GO
ALTER TABLE `preg_contact_us` ADD FOREIGN KEY (`user_id`) REFERENCES `preg_user` (`id`);
GO
ALTER TABLE `preg_contact_us` CHECK CONSTRAINT `FK__preg_cota__user___05D8E0BE`
GO
ALTER TABLE `preg_contraction` ADD FOREIGN KEY (`user_id`) REFERENCES `preg_user` (`id`);
GO
ALTER TABLE `preg_contraction` CHECK CONSTRAINT `FK_preg_contraction_preg_user`
GO
ALTER TABLE `preg_customer_response` ADD FOREIGN KEY (`user_id`) REFERENCES `preg_user` (`id`);
GO
ALTER TABLE `preg_customer_response` CHECK CONSTRAINT `FK_preg_customer_response_preg_user`
GO
ALTER TABLE `preg_customer_response` ADD FOREIGN KEY (`answer_user_id`) REFERENCES `preg_user` (`id`);
GO
ALTER TABLE `preg_customer_response` CHECK CONSTRAINT `FK_preg_customer_response_preg_user1`
GO
ALTER TABLE `preg_daily_interact` ADD FOREIGN KEY (`daily_id`) REFERENCES `preg_daily` (`id`);
GO
ALTER TABLE `preg_daily_interact` CHECK CONSTRAINT `FK__preg_dail__daily__03F0984C`
GO
ALTER TABLE `preg_daily_interact` ADD FOREIGN KEY (`user_id`) REFERENCES `preg_user` (`id`);
GO
ALTER TABLE `preg_daily_interact` CHECK CONSTRAINT `FK__preg_dail__user___02FC7413`
GO
ALTER TABLE `preg_faq_answer` ADD FOREIGN KEY (`faq_id`) REFERENCES `preg_faq` (`id`);
GO
ALTER TABLE `preg_faq_answer` CHECK CONSTRAINT `FK_preg_faq_answer_preg_faq`
GO
ALTER TABLE `preg_guides` ADD FOREIGN KEY (`guides_type_id`) REFERENCES `preg_guides_type` (`id`);
GO
ALTER TABLE `preg_guides` CHECK CONSTRAINT `FK__preg_guid__guide__778AC167`
GO
ALTER TABLE `preg_guides` ADD FOREIGN KEY (`page_id`) REFERENCES `preg_page` (`id`);
GO
ALTER TABLE `preg_guides` CHECK CONSTRAINT `FK__preg_guid__page___787EE5A0`
GO
ALTER TABLE `preg_help` ADD FOREIGN KEY (`help_category_id`) REFERENCES `preg_help_category` (`id`);
GO
ALTER TABLE `preg_help` CHECK CONSTRAINT `FK__preg_help__help___6754599E`
GO
ALTER TABLE `preg_hospital_bag_item` ADD FOREIGN KEY (`custom_item_by_user_id`) REFERENCES `preg_user` (`id`);
GO
ALTER TABLE `preg_hospital_bag_item` CHECK CONSTRAINT `FK_preg_hospital_bag_item_preg_user`
GO
ALTER TABLE `preg_image` ADD FOREIGN KEY (`image_type_id`) REFERENCES `preg_image_type` (`id`);
GO
ALTER TABLE `preg_image` CHECK CONSTRAINT `FK__preg_imag__image__7C4F7684`
GO
ALTER TABLE `preg_image` ADD FOREIGN KEY (`week_id`) REFERENCES `preg_week` (`id`);
GO
ALTER TABLE `preg_image` CHECK CONSTRAINT `FK__preg_imag__week___7B5B524B`
GO
ALTER TABLE `preg_medical_package_test` ADD FOREIGN KEY (`medical_service_package_id`) REFERENCES `preg_medical_service_package` (`id`);
GO
ALTER TABLE `preg_medical_package_test` CHECK CONSTRAINT `FK_preg_medical_package_test_preg_medical_service_package1`
GO
ALTER TABLE `preg_medical_package_test` ADD FOREIGN KEY (`medical_test_id`) REFERENCES `preg_medical_test` (`id`);
GO
ALTER TABLE `preg_medical_package_test` CHECK CONSTRAINT `FK_preg_medical_package_test_preg_medical_test`
GO
ALTER TABLE `preg_my_belly` ADD FOREIGN KEY (`user_id`) REFERENCES `preg_user` (`id`);
GO
ALTER TABLE `preg_my_belly` CHECK CONSTRAINT `FK__preg_my_b__user___74AE54BC`
GO
ALTER TABLE `preg_my_birth_plan` ADD FOREIGN KEY (`my_birth_plan_item_id`) REFERENCES `preg_my_birth_plan_item` (`id`);
GO
ALTER TABLE `preg_my_birth_plan` CHECK CONSTRAINT `FK_preg_my_birth_plan_preg_my_birth_plan_item2`
GO
ALTER TABLE `preg_my_birth_plan` ADD FOREIGN KEY (`user_id`) REFERENCES `preg_user` (`id`);
GO
ALTER TABLE `preg_my_birth_plan` CHECK CONSTRAINT `FK_preg_my_birth_plan_preg_user`
GO
ALTER TABLE `preg_my_birth_plan_item` ADD FOREIGN KEY (`my_birth_plan_type_id`) REFERENCES `preg_my_birth_plan_type` (`id`);
GO
ALTER TABLE `preg_my_birth_plan_item` CHECK CONSTRAINT `FK_preg_my_birth_plan_item_preg_my_birth_plan_type`
GO
ALTER TABLE `preg_my_birth_plan_item` ADD FOREIGN KEY (`custom_item_by_user_id`) REFERENCES `preg_user` (`id`);
GO
ALTER TABLE `preg_my_birth_plan_item` CHECK CONSTRAINT `FK_preg_my_birth_plan_item_preg_user`
GO
ALTER TABLE `preg_my_weight` ADD FOREIGN KEY (`my_weight_type_id`) REFERENCES `preg_my_weight_unit` (`id`);
GO
ALTER TABLE `preg_my_weight` CHECK CONSTRAINT `FK__preg_my_w__my_we__76969D2E`
GO
ALTER TABLE `preg_my_weight` ADD FOREIGN KEY (`user_id`) REFERENCES `preg_user` (`id`);
GO
ALTER TABLE `preg_my_weight` CHECK CONSTRAINT `FK__preg_my_w__user___75A278F5`
GO
ALTER TABLE `preg_phone` ADD FOREIGN KEY (`profession_id`) REFERENCES `preg_profession_type` (`id`);
GO
ALTER TABLE `preg_phone` CHECK CONSTRAINT `FK__preg_phon__profe__6477ECF3`
GO
ALTER TABLE `preg_phone` ADD FOREIGN KEY (`user_id`) REFERENCES `preg_user` (`id`);
GO
ALTER TABLE `preg_phone` CHECK CONSTRAINT `FK__preg_phon__user___656C112C`
GO
ALTER TABLE `preg_pregnancy` ADD FOREIGN KEY (`user_id`) REFERENCES `preg_user` (`id`);
GO
ALTER TABLE `preg_pregnancy` CHECK CONSTRAINT `FK__preg_preg__user___06CD04F7`
GO
ALTER TABLE `preg_profession` ADD FOREIGN KEY (`profession_type_id`) REFERENCES `preg_profession_type` (`id`);
GO
ALTER TABLE `preg_profession` CHECK CONSTRAINT `FK_preg_profession_preg_profession_type`
GO
ALTER TABLE `preg_profession` ADD FOREIGN KEY (`user_id`) REFERENCES `preg_user` (`id`);
GO
ALTER TABLE `preg_profession` CHECK CONSTRAINT `FK_preg_profession_preg_user`
GO
ALTER TABLE `preg_question` ADD FOREIGN KEY (`question_type_id`) REFERENCES `preg_question_type` (`id`);
GO
ALTER TABLE `preg_question` CHECK CONSTRAINT `FK_preg_question_preg_question_type`
GO
ALTER TABLE `preg_question` ADD FOREIGN KEY (`custom_question_by_user_id`) REFERENCES `preg_user` (`id`);
GO
ALTER TABLE `preg_question` CHECK CONSTRAINT `FK_preg_question_preg_user`
GO
ALTER TABLE `preg_setting` ADD FOREIGN KEY (`user_id`) REFERENCES `preg_user` (`id`);
GO
ALTER TABLE `preg_setting` CHECK CONSTRAINT `FK__preg_sett__user___07C12930`
GO
ALTER TABLE `preg_shopping_item` ADD FOREIGN KEY (`category_id`) REFERENCES `preg_shopping_category` (`id`);
GO
ALTER TABLE `preg_shopping_item` CHECK CONSTRAINT `FK_preg_shopping_item_preg_shopping_category`
GO
ALTER TABLE `preg_shopping_item` ADD FOREIGN KEY (`custom_item_by_user_id`) REFERENCES `preg_user` (`id`);
GO
ALTER TABLE `preg_shopping_item` CHECK CONSTRAINT `FK_preg_shopping_item_preg_user`
GO
ALTER TABLE `preg_size_guide` ADD FOREIGN KEY (`week_id`) REFERENCES `preg_week` (`id`);
GO
ALTER TABLE `preg_size_guide` CHECK CONSTRAINT `FK_preg_size_guide_preg_week`
GO
ALTER TABLE `preg_time_line` ADD FOREIGN KEY (`time_frame_id`) REFERENCES `preg_time_frame` (`id`);
GO
ALTER TABLE `preg_time_line` CHECK CONSTRAINT `FK__preg_time__time___7A672E12`
GO
ALTER TABLE `preg_time_line` ADD FOREIGN KEY (`week_id`) REFERENCES `preg_week` (`id`);
GO
ALTER TABLE `preg_time_line` CHECK CONSTRAINT `FK__preg_time__week___797309D9`
GO
ALTER TABLE `preg_todo` ADD FOREIGN KEY (`custom_task_by_user_id`) REFERENCES `preg_user` (`id`);
GO
ALTER TABLE `preg_todo` CHECK CONSTRAINT `FK__preg_todo__user___6A30C649`
GO
ALTER TABLE `preg_todo` ADD FOREIGN KEY (`day_id`) REFERENCES `preg_daily` (`id`);
GO
ALTER TABLE `preg_todo` CHECK CONSTRAINT `FK_preg_todo_preg_daily`
GO
ALTER TABLE `preg_upgrade` ADD FOREIGN KEY (`user_id`) REFERENCES `preg_user` (`id`);
GO
ALTER TABLE `preg_upgrade` CHECK CONSTRAINT `FK__preg_upgr__user___08B54D69`
GO
ALTER TABLE `preg_user` ADD FOREIGN KEY (`social_type_id`) REFERENCES `preg_social_type` (`id`);
GO
ALTER TABLE `preg_user` CHECK CONSTRAINT `FK_preg_user_preg_social_type`
GO
ALTER TABLE `preg_user_baby_name` ADD FOREIGN KEY (`baby_name_id`) REFERENCES `preg_baby_name` (`id`);
GO
ALTER TABLE `preg_user_baby_name` CHECK CONSTRAINT `FK_preg_user_baby_name_preg_baby_name2`
GO
ALTER TABLE `preg_user_baby_name` ADD FOREIGN KEY (`user_id`) REFERENCES `preg_user` (`id`);
GO
ALTER TABLE `preg_user_baby_name` CHECK CONSTRAINT `FK_preg_user_baby_name_preg_user`
GO
ALTER TABLE `preg_user_hospital_bag_item` ADD FOREIGN KEY (`hospital_bag_item_id`) REFERENCES `preg_hospital_bag_item` (`id`);
GO
ALTER TABLE `preg_user_hospital_bag_item` CHECK CONSTRAINT `FK_preg_user_hospital_bag_item_preg_hospital_bag_item`
GO
ALTER TABLE `preg_user_hospital_bag_item` ADD FOREIGN KEY (`user_id`) REFERENCES `preg_user` (`id`);
GO
ALTER TABLE `preg_user_hospital_bag_item` CHECK CONSTRAINT `FK_preg_user_hospital_bag_item_preg_user`
GO
ALTER TABLE `preg_user_kick_history` ADD FOREIGN KEY (`user_id`) REFERENCES `preg_user` (`id`);
GO
ALTER TABLE `preg_user_kick_history` CHECK CONSTRAINT `FK_preg_user_kick_history_preg_user`
GO
ALTER TABLE `preg_user_kick_history` ADD FOREIGN KEY (`kick_result_id`) REFERENCES `preg_kick_result` (`id`);
GO
ALTER TABLE `preg_user_kick_history` CHECK CONSTRAINT `FK_preg_user_kick_history_preg_user_kick_history`
GO
ALTER TABLE `preg_user_medical_service_package` ADD FOREIGN KEY (`medical_service_package_id`) REFERENCES `preg_medical_service_package` (`id`);
GO
ALTER TABLE `preg_user_medical_service_package` CHECK CONSTRAINT `FK_preg_user_medical_service_package_preg_medical_service_package`
GO
ALTER TABLE `preg_user_medical_service_package` ADD FOREIGN KEY (`user_id`) REFERENCES `preg_user` (`id`);
GO
ALTER TABLE `preg_user_medical_service_package` CHECK CONSTRAINT `FK_preg_user_medical_service_package_preg_user`
GO
ALTER TABLE `preg_user_shopping_cart` ADD FOREIGN KEY (`shopping_item_id`) REFERENCES `preg_shopping_item` (`id`);
GO
ALTER TABLE `preg_user_shopping_cart` CHECK CONSTRAINT `FK_preg_user_shopping_cart_preg_shopping_item`
GO
ALTER TABLE `preg_user_shopping_cart` ADD FOREIGN KEY (`user_id`) REFERENCES `preg_user` (`id`);
GO
ALTER TABLE `preg_user_shopping_cart` CHECK CONSTRAINT `FK_preg_user_shopping_cart_preg_user`
GO
ALTER TABLE `preg_user_todo` ADD FOREIGN KEY (`todo_id`) REFERENCES `preg_todo` (`id`);
GO
ALTER TABLE `preg_user_todo` CHECK CONSTRAINT `FK_preg_user_todo_preg_todo`
GO
ALTER TABLE `preg_user_todo` ADD FOREIGN KEY (`user_id`) REFERENCES `preg_user` (`id`);
GO
ALTER TABLE `preg_user_todo` CHECK CONSTRAINT `FK_preg_user_todo_preg_user`
GO
ALTER TABLE `preg_weekly_interact` ADD FOREIGN KEY (`user_id`) REFERENCES `preg_user` (`id`);
GO
ALTER TABLE `preg_weekly_interact` CHECK CONSTRAINT `FK__preg_week__user___7E37BEF6`
GO
ALTER TABLE `preg_weekly_interact` ADD FOREIGN KEY (`week_id`) REFERENCES `preg_week` (`id`);
GO
ALTER TABLE `preg_weekly_interact` CHECK CONSTRAINT `FK__preg_week__week___7D439ABD`
GO
