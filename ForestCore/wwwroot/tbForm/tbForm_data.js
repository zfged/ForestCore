tbForm_data = new Object();
tbForm_data[1393]='{"0":{"t":"head","v":"{\\"cpt_val\\":\\"Заказать обратный звонок\\",\\"class_val\\":\\"\\",\\"classcn_val\\":\\"\\",\\"msg_OK\\":\\"Ваше сообщение получено.\\\\nВскоре Мы свяжемся с Вами \\",\\"msg_ERR\\":\\"Сервис временно недоступен.\\\\nВоспользуйтесь другими способами связи.\\",\\"cpt_cbx\\":false,\\"class_cbx\\":false,\\"classcn_cbx\\":false}"},"1":{"t":"txt","v":"{\\"name\\":\\"Name_K\\",\\"type\\":\\"txt\\",\\"label_val\\":\\"Ваше имя:\\",\\"placeholder_val\\":\\"\\",\\"value_val\\":\\"\\",\\"class_val\\":\\"\\",\\"classcn_val\\":\\"\\",\\"mail_txt\\":\\"Имя\\",\\"label_cbx\\":true,\\"placeholder_cbx\\":false,\\"value_cbx\\":false,\\"needs_cbx\\":true,\\"readonly_cbx\\":false,\\"class_cbx\\":false,\\"classcn_cbx\\":false}"},"2":{"t":"txt","v":"{\\"name\\":\\"Fone\\",\\"type\\":\\"tel\\",\\"label_val\\":\\"Номер Вашего телефона:\\",\\"placeholder_val\\":\\"Подсказка\\",\\"value_val\\":\\"\\",\\"class_val\\":\\"\\",\\"classcn_val\\":\\"\\",\\"mail_txt\\":\\"Телефон\\",\\"label_cbx\\":true,\\"placeholder_cbx\\":false,\\"value_cbx\\":false,\\"needs_cbx\\":true,\\"readonly_cbx\\":false,\\"class_cbx\\":false,\\"classcn_cbx\\":false}"},"3":{"t":"btn","v":"{\\"name\\":\\"submit\\",\\"bt_text\\":\\"Жду звонка\\",\\"class_val\\":\\"\\",\\"classcn_val\\":\\"\\",\\"captcha\\":false,\\"class_cbx\\":false,\\"classcn_cbx\\":false}"}}';



tbForm_data2 = new Object();
	for (var f in tbForm_data) { 
		lines=jQuery.parseJSON(tbForm_data[f]);
		
		var tmp= new Object();
		for (var l in lines){
			var tl = new Object();
			tl["t"]=lines[l].t;
			tl["v"]=jQuery.parseJSON(lines[l].v);
			tmp[l]=tl;
		}
		tbForm_data2[f]=tmp;
	}