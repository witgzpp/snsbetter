select * from talk;
select * from user;

delete from user;



delimiter $$

use `social`$$

drop trigger /*!50032 IF EXISTS */ `tri_talk_update`$$

create
    /*!50017 DEFINER = 'root'@'localhost' */
    trigger `tri_talk_update` after update on `talk` 
    for each row begin
    if old.cname!=new.uname then
    
	insert into utalk(oldcontent,newcontent,talkid,cdate,updateuname) values(old.content,new.content,old.id,NOW(),new.uname);
	end if;
    
    
end;
$$

delimiter ;



delimiter $$

use `social`$$

drop trigger /*!50032 IF EXISTS */ `tri_user_insert`$$

create
    /*!50017 DEFINER = 'root'@'localhost' */
    trigger `tri_user_insert` after insert on `user` 
    for each row begin
declare allvalue nvarchar(500);
declare username nvarchar(20);
declare userpwd nvarchar(60);
declare userid int(11);
set username=new.uname;
set userpwd=new.upwd;
set userid=new.id;
set allvalue =CONCAT(username," is created,  MD5 password of 32bit is ",userpwd);
insert into user_log(content,cdate,uid) values(allvalue,NOW(),userid);
end;
$$

delimiter ;


delimiter $$

use `social`$$

drop trigger /*!50032 IF EXISTS */ `tri_user_signal`$$

create
    /*!50017 DEFINER = 'root'@'localhost' */
    trigger `tri_user_signal` after delete on `user` 
    for each row insert into user_log(content,cdate,uid) values(CONCAT(old.uname," is deleted"),NOW(),old.id);
$$

delimiter ;





select * from comment;
