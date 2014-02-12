TwitterFeed
===========

A module to display twitter feeds on your DNN site

The module can only run in DNN versions gretaer than 7.2.1

Features:
- client side rendering, if enabled in modulesettings
- auto-refresh (only in client side mode)
- fully template based


Note that you must sign up for a Twitter API key in order to use that module. You may do so on https://dev.twitter.com/. It's free and only takes minutes.

If you want to see the module in action, visit http://dnn-connect.org/community

The module is fully template based and is being delivered with two base themes. A theme consists of four html- and one css-file, all contained within a single folder, where the name of the folder makes the theme name.

Go to /Desktopmodules/Connect/TwitterFeed/Templates and you will notice the two base theme folders. To create new theme, you simply copy one of those folders into a new folder at the same position. Then you go into the settings of the module and select your new folder as the template for the current module instance.

The structure within the folder is as follows:

- header.htm. This is rendered at first, be default it's used for opening the main div around the feeds
- item.htm. Represents a single feed
- itemAlternate.htm. Represents a single feed, used in alternating order with item.htm
- footer.htm. Rendered as the last template, by default used to close the main div around feeds and to display a link to twitter.

Supported Tokens (Item Templates)

- CONTENT   renders the text of the post, without any link markup
- HTMLCONTENT   renders the html formatted content of a single post
- SOURCE        renders the device / source that a post has been published from (e.g. Twitter for iPad)
- HASEMBEDDEDIMAGE and /HASEMBEDDEDIMAGE renders everything in between if the post contains an image
- EMBEDDEDIMAGEURL  renders the url for the first embedded image of a post
- HASEMBEDDELINK and /HASEMBEDDELINK renders everything in between if a post contains a link
- EMBEDDEDLINKURL    renders the url of the first embeddded link
- PUBLISHDATE   renders that date part of the publishdate (if today, it will render "today", if yesterday it will render "yesterday"
- PUBLISHTIME   renders the time part of the publishdate (if within the last hour it will render xx minutes ago)
- AUTHORSCREENNAME  renders the screen name of the poster
- AUTHORFULLNAME renders the full name of the poster
- AUTHORURL renders the url of the poster's website (if applicable)
- AUTHORIMAGEURL  renders the url to the poster's profile pic
- AUTHORIMAGEURLBIGGER renders the url to the poster's profile pic in a bigger size
- AUTHORIMAGEURLORIGINAL renders the url to the poster's profile pic in its original size as uploaded to twitter


Supported Tokens (Header / Footer Templates)

- TWITTERURL  renders the url to twitter, depending on the current settings (i.e. if in searchmode, then to current search. If in Usertimeline mode then to the current timeline)
- RESX:XXXX renders a localized string from the module's resourcefile where XXXX is the key to the resource (TWITTERLINK being the only key supported currently)
