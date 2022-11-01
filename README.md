# YSGM

A CLI tool to manage official genshin servers

## Setup
Edit the configuration file included to your MUIP server IP and your servers SSH credentials.
![image](https://user-images.githubusercontent.com/27217031/199267587-c7d1f8ed-535e-49e6-ae63-ef8c16a46086.png)
SSH credentials are not required if you only want to use GM / MUIP related commands.

## Commands

### shell
```html
> shell cat gameserver/conf/gameserver.xml | grep GmTalk
    <GmTalk open="true" />
```

### sql
```html
> sql hk4e_db_user_32live select * from t_player_data_0;

<row>
    <field name="uid">10000</field>
    <field name="nickname">Aqua</field>
    <field name="level">56</field>
    <field name="exp">8160</field>
    <field name="vip_point">0</field>
    <field name="json_data">{ "is_proficient_player": true}</field>
    <field name="bin_data">0x...</field>
    <field name="extra_bin_data">0x...</field>
    <field name="data_version">887</field>
    <field name="tag_list"></field>
    <field name="create_time">2022-10-26 12:25:05</field>
    <field name="last_save_time">2022-10-31 22:47:35</field>
    <field name="is_delete">0</field>
    <field name="reserved_1">0</field>
    <field name="reserved_2">0</field>
    <field name="before_login_bin_data">0x...</field>
</row>
```

### gm
Executes a GM command on the specified UID
```jsonc
// gm <uid> <cmd>
> gm 10000 KILL SELF
{"data":{"msg":"KILL SELF","retmsg":"KILL SELF"},"msg":"succ","retcode":0,"ticket":"YSGM@1667316780"}
```

### muip
Make your own custom MUIP query
```
Usage: muip <cmd_id> [key=value,key2=value2]
```

### pull
Pulls player data from the database and saves it
```
Usage: pull <uid>
```

### push
Pushes player data to the database
```
Usage: push <uid>
```
